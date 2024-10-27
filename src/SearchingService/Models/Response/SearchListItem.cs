﻿namespace SearchingService.Models.Response;

public record SearchListItem(
    long Id,
    string UserId,
    string GuildId,
    string ChannelId,
    string Content,
    DateTime CreatedAtUtc,
    DateTime ValidToUtc
)
{
    public bool IsInvalid => ValidToUtc <= DateTime.UtcNow;
}