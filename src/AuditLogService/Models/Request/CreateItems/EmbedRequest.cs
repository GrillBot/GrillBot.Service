﻿using System.ComponentModel.DataAnnotations;
using Discord;

namespace AuditLogService.Models.Request.CreateItems;

public class EmbedRequest
{
    public string? Title { get; set; }

    [Required]
    [StringLength(16)]
    public string Type { get; set; } = null!;

    public string? ImageInfo { get; set; }
    public string? VideoInfo { get; set; }
    public string? AuthorName { get; set; }
    public bool ContainsFooter { get; set; }
    public string? ProviderName { get; set; }
    public string? ThumbnailInfo { get; set; }
    public List<EmbedFieldBuilder> Fields { get; set; } = new();
}
