using AgentShield.Application.Services;
using AgentShield.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgentShield.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditController : ControllerBase
{
    private readonly AuditLogService _auditLogService;

    public AuditController(AuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    [HttpGet]
    public ActionResult<List<SecurityAuditLog>> GetAllLogs()
    {
        return Ok(_auditLogService.GetAllLogs());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<SecurityAuditLog> GetLogById(Guid id)
    {
        var log = _auditLogService.GetLogById(id);

        if (log == null)
        {
            return NotFound();
        }

        return Ok(log);
    }
}