﻿using System.ComponentModel.DataAnnotations;
using Discord;

namespace AuditLogService.Models.Request.CreateItems;

public class LogMessageRequest
{
    [Required]
    public string Message { get; set; } = null!;

    [Required]
    public LogSeverity Severity { get; set; }
}