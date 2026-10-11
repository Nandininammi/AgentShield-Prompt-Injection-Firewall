using AgentShield.Application.Services;
using AgentShield.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgentShield.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AuditLogService _auditLogService;

    public DashboardController(AuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        var logs = _auditLogService.GetAllLogs();

        var summary = new
        {
            TotalScans = logs.Count,

            ThreatsBlocked = logs.Count(x =>
                x.Action.Equals("Block", StringComparison.OrdinalIgnoreCase)),

            RequestsAllowed = logs.Count(x =>
                x.Action.Equals("Allow", StringComparison.OrdinalIgnoreCase)),

            RequestsForReview = logs.Count(x =>
                x.Action.Equals("Review", StringComparison.OrdinalIgnoreCase))
        };

        return Ok(summary);
    }

    [HttpGet("attack-types")]
    public IActionResult GetAttackTypes()
    {
        var logs = _auditLogService.GetAllLogs();

        var attackTypes = logs
            .SelectMany(x => x.AttackTypes)
            .GroupBy(x => x)
            .Select(g => new
            {
                AttackType = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();

        return Ok(attackTypes);
    }

    [HttpGet("severity")]
    public IActionResult GetSeverityStatistics()
    {
        var logs = _auditLogService.GetAllLogs();

        var severityStatistics = logs
            .GroupBy(x => x.Severity)
            .Select(g => new
            {
                Severity = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();

        return Ok(severityStatistics);
    }

    [HttpGet("recent-events")]
    public IActionResult GetRecentEvents()
    {
        var logs = _auditLogService.GetAllLogs();

        var recentEvents = logs
            .Take(10)
            .Select(x => new
            {
                x.Id,
                x.Timestamp,
                x.Content,
                x.ThreatDetected,
                x.AttackTypes,
                x.Severity,
                x.Confidence,
                x.Action,
                x.DetectionMethod,
                x.Explanation
            })
            .ToList();

        return Ok(recentEvents);
    }
}