﻿using GrillBot.Core.Database.Repository;
using GrillBot.Core.Managers.Performance;
using Microsoft.EntityFrameworkCore;
using RubbergodService.Core.Entity;

namespace RubbergodService.Core.Repository;

public class StatisticsRepository : RepositoryBase<RubbergodServiceContext>
{
    public StatisticsRepository(RubbergodServiceContext context, ICounterManager counterManager) : base(context, counterManager)
    {
    }

    public async Task<Dictionary<string, long>> GetStatisticsAsync()
    {
        using (CreateCounter())
        {
            return new Dictionary<string, long>
            {
                { nameof(Context.Karma), await Context.Karma.CountAsync() },
                { nameof(Context.MemberCache), await Context.MemberCache.CountAsync() }
            };
        }
    }
}