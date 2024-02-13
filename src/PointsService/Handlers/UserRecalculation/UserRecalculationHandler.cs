﻿using GrillBot.Core.Managers.Performance;
using GrillBot.Core.RabbitMQ.Publisher;
using PointsService.Core.Entity;
using PointsService.Handlers.Abstractions;
using PointsService.Models.Events;

namespace PointsService.Handlers.UserRecalculation;

public partial class UserRecalculationHandler : BaseEventWithDb<UserRecalculationPayload>
{
    public override string QueueName => UserRecalculationPayload.QueueName;

    public UserRecalculationHandler(ILoggerFactory loggerFactory, PointsServiceContext dbContext, ICounterManager counterManager, IRabbitMQPublisher publisher)
        : base(loggerFactory, dbContext, counterManager, publisher)
    {
    }

    protected override async Task HandleInternalAsync(UserRecalculationPayload payload)
    {
        var user = await FindUserAsync(payload.GuildId, payload.UserId);
        if (user is null)
            return;

        await ProcessActionAsync(ComputeUserInfoAsync, user, nameof(ComputeUserInfoAsync));
        await ProcessActionAsync(ComputeDailyStatsAsync, user, nameof(ComputeDailyStatsAsync));
        await ProcessActionAsync(ComputeLeaderboardAsync, user, nameof(ComputeLeaderboardAsync));
        await ProcessActionAsync(ComputePositionAsync, user, nameof(ComputePositionAsync));
    }

    private async Task ProcessActionAsync(Func<User, Task> action, User user, string actionName)
    {
        actionName = actionName.Replace("Async", "");

        using (CreateCounter(actionName))
            await action(user);

        using (CreateCounter("Database"))
            await DbContext.SaveChangesAsync();
    }
}
