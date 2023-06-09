﻿using AuditLogService.Core.Entity;
using AuditLogService.Core.Enums;
using AuditLogService.Models.Request.Search;
using Discord;
using GrillBot.Core.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace AuditLogService.Actions.Search;

public partial class SearchItemsAction
{
    private async Task<List<Guid>> SearchIdsFromAdvancedFilterAsync(SearchRequest request)
    {
        var result = new List<Guid>();
        if (request.Ids.Count > 0 || request.ShowTypes.Count == 0 || request.AdvancedSearch is null)
            return result; // Ignore advanced filters if IDs was specified explicitly.

        if (request.IsAdvancedFilterSet(LogType.Info))
        {
            result.AddRange(
                await Context.LogMessages.AsNoTracking()
                    .Where(o => o.Message.Contains(request.AdvancedSearch.Info!.Text!) && o.Severity == LogSeverity.Info)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.Warning))
        {
            result.AddRange(
                await Context.LogMessages.AsNoTracking()
                    .Where(o => o.Message.Contains(request.AdvancedSearch.Warning!.Text!) && o.Severity == LogSeverity.Warning)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.Warning))
        {
            result.AddRange(
                await Context.LogMessages.AsNoTracking()
                    .Where(o => o.Message.Contains(request.AdvancedSearch.Error!.Text!) && o.Severity == LogSeverity.Error)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.InteractionCommand))
        {
            var baseQuery = Context.InteractionCommands.AsNoTracking();
            var searchReq = request.AdvancedSearch.Interaction!;

            if (!string.IsNullOrEmpty(searchReq.ActionName))
                baseQuery = baseQuery.Where(o => $"{o.Name} ({o.ModuleName}/{o.MethodName})".Contains(searchReq.ActionName));
            if (searchReq.Success is not null)
                baseQuery = baseQuery.Where(o => o.IsSuccess == searchReq.Success);
            if (searchReq.DurationFrom is not null)
                baseQuery = baseQuery.Where(o => o.Duration >= searchReq.DurationFrom.Value);
            if (searchReq.DurationTo is not null)
                baseQuery = baseQuery.Where(o => o.Duration <= searchReq.DurationTo.Value);

            result.AddRange(await baseQuery.Select(o => o.LogItemId).ToListAsync());
        }

        if (request.IsAdvancedFilterSet(LogType.JobCompleted))
        {
            var baseQuery = Context.JobExecutions.AsNoTracking();
            var searchReq = request.AdvancedSearch.Job!;

            if (!string.IsNullOrEmpty(searchReq.ActionName))
                baseQuery = baseQuery.Where(o => o.JobName.Contains(searchReq.ActionName));
            if (searchReq.Success is not null)
                baseQuery = baseQuery.Where(o => o.WasError != searchReq.Success);
            if (searchReq.DurationFrom is not null)
                baseQuery = baseQuery.Where(o => (o.EndAt - o.StartAt).TotalMilliseconds >= searchReq.DurationFrom.Value);
            if (searchReq.DurationTo is not null)
                baseQuery = baseQuery.Where(o => (o.EndAt - o.StartAt).TotalMilliseconds <= searchReq.DurationTo.Value);

            result.AddRange(await baseQuery.Select(o => o.LogItemId).ToListAsync());
        }

        if (request.IsAdvancedFilterSet(LogType.Api))
        {
            var baseQuery = Context.ApiRequests.AsNoTracking();
            var searchReq = request.AdvancedSearch.Api!;

            if (!string.IsNullOrEmpty(searchReq.ControllerName))
                baseQuery = baseQuery.Where(o => o.ControllerName.Contains(searchReq.ControllerName));
            if (!string.IsNullOrEmpty(searchReq.ActionName))
                baseQuery = baseQuery.Where(o => o.ActionName.Contains(searchReq.ActionName));
            if (!string.IsNullOrEmpty(searchReq.PathTemplate))
                baseQuery = baseQuery.Where(o => o.TemplatePath.Contains(searchReq.PathTemplate));
            if (searchReq.DurationFrom is not null)
                baseQuery = baseQuery.Where(o => (o.EndAt - o.StartAt).TotalMilliseconds >= searchReq.DurationFrom.Value);
            if (searchReq.DurationTo is not null)
                baseQuery = baseQuery.Where(o => (o.EndAt - o.StartAt).TotalMilliseconds <= searchReq.DurationTo.Value);
            if (!string.IsNullOrEmpty(searchReq.Method))
                baseQuery = baseQuery.Where(o => o.Method == searchReq.Method);
            if (!string.IsNullOrEmpty(searchReq.ApiGroupName))
                baseQuery = baseQuery.Where(o => o.ApiGroupName == searchReq.ApiGroupName);

            result.AddRange(await baseQuery.Select(o => o.LogItemId).ToListAsync());
        }

        if (request.IsAdvancedFilterSet(LogType.OverwriteCreated))
        {
            result.AddRange(
                await Context.OverwriteCreatedItems.AsNoTracking()
                    .Where(o => o.OverwriteInfo.TargetId == request.AdvancedSearch.OverwriteCreated!.UserId)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.OverwriteDeleted))
        {
            result.AddRange(
                await Context.OverwriteDeletedItems.AsNoTracking()
                    .Where(o => o.OverwriteInfo.TargetId == request.AdvancedSearch.OverwriteDeleted!.UserId)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.OverwriteUpdated))
        {
            result.AddRange(
                await Context.OverwriteUpdatedItems.AsNoTracking()
                    .Where(o => o.Before.TargetId == request.AdvancedSearch.OverwriteUpdated!.UserId || o.After.TargetId == request.AdvancedSearch.OverwriteUpdated!.UserId)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.MemberRoleUpdated))
        {
            result.AddRange(
                await Context.MemberRoleUpdatedItems.AsNoTracking()
                    .Where(o => o.UserId == request.AdvancedSearch.MemberRolesUpdated!.UserId)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.MemberUpdated))
        {
            result.AddRange(
                await Context.MemberUpdatedItems.AsNoTracking()
                    .Where(o => o.Before.UserId == request.AdvancedSearch.MemberUpdated!.UserId || o.After.UserId == request.AdvancedSearch.MemberUpdated!.UserId)
                    .Select(o => o.LogItemId)
                    .ToListAsync()
            );
        }

        if (request.IsAdvancedFilterSet(LogType.MessageDeleted))
        {
            var baseQuery = Context.MessageDeletedItems.AsNoTracking();
            var searchReq = request.AdvancedSearch.MessageDeleted!;

            if (searchReq.ContainsEmbed is not null)
                baseQuery = searchReq.ContainsEmbed.Value ? baseQuery.Where(o => o.Embeds.Count > 0) : baseQuery.Where(o => o.Embeds.Count == 0);
            if (!string.IsNullOrEmpty(searchReq.ContentContains))
                baseQuery = baseQuery.Where(o => !string.IsNullOrEmpty(o.Content) && o.Content.Contains(searchReq.ContentContains));
            if (!string.IsNullOrEmpty(searchReq.AuthorId))
                baseQuery = baseQuery.Where(o => o.AuthorId == searchReq.AuthorId);

            result.AddRange(await baseQuery.Select(o => o.LogItemId).ToListAsync());
        }

        return result.Distinct().ToList();
    }

    private async Task<PaginatedResponse<LogItem>> ReadLogHeaders(SearchRequest request)
    {
        var query = Context.LogItems.Include(o => o.Files).AsNoTracking();

        if (!string.IsNullOrEmpty(request.GuildId))
            query = query.Where(o => o.GuildId == request.GuildId);
        if (request.UserIds.Count > 0)
            query = query.Where(o => !string.IsNullOrEmpty(o.UserId) && request.UserIds.Contains(o.UserId));
        if (request.ShowTypes.Count > 0)
            query = query.Where(o => request.ShowTypes.Contains(o.Type));
        else if (request.IgnoreTypes.Count > 0)
            query = query.Where(o => !request.IgnoreTypes.Contains(o.Type));
        if (request.CreatedFrom is not null)
            query = query.Where(o => o.CreatedAt >= request.CreatedFrom.Value);
        if (request.CreatedTo is not null)
            query = query.Where(o => o.CreatedAt <= request.CreatedTo.Value);
        if (request.OnlyWithFiles)
            query = query.Where(o => o.Files.Count > 0);
        if (request.Ids.Count > 0)
            query = query.Where(o => request.Ids.Contains(o.Id));

        query = request.Sort.Descending ? query.OrderByDescending(o => o.CreatedAt) : query.OrderBy(o => o.CreatedAt);
        return await PaginatedResponse<LogItem>.CreateWithEntityAsync(query, request.Pagination);
    }
}
