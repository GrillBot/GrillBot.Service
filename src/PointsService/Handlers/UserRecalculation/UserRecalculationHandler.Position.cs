﻿using Microsoft.EntityFrameworkCore;
using PointsService.Core.Entity;

namespace PointsService.Handlers.UserRecalculation;

public partial class UserRecalculationHandler
{
    private async Task ComputePositionAsync(User user)
    {
        var position = await ComputeActualPositionAsync(user);
        if (position == 0)
        {
            using (CreateCounter("Database"))
                position = await DbContext.Leaderboard.AsNoTracking().CountAsync(o => o.GuildId == user.GuildId);
        }

        user.PointsPosition = position;

        var samePositionUsers = await FindUsersWithSamePositionAsync(user, position);
        foreach (var samePositionUser in samePositionUsers)
            await EnqueueUserForRecalculationAsync(samePositionUser.GuildId, samePositionUser.Id);
    }

    private async Task<int> ComputeActualPositionAsync(User user)
    {
        var userIdsQuery = DbContext.Leaderboard.AsNoTracking()
            .Where(o => o.GuildId == user.GuildId)
            .OrderByDescending(o => o.YearBack)
            .Select(o => o.UserId);

        List<string> userIds;
        using (CreateCounter("Database"))
            userIds = await userIdsQuery.ToListAsync();

        return userIds.FindIndex(o => o == user.Id) + 1;
    }

    private async Task<List<User>> FindUsersWithSamePositionAsync(User user, int position)
    {
        using (CreateCounter("Database"))
        {
            return await DbContext.Users
                .Where(o =>
                    o.GuildId == user.GuildId &&
                    o.Id != user.Id &&
                    o.PointsPosition == position &&
                    DbContext.Leaderboard.Any(l => l.GuildId == o.GuildId && l.UserId == o.Id && l.YearBack > 0)
                )
                .ToListAsync();
        }
    }
}
