using AgentShield.Application.Interfaces;
using AgentShield.Application.Services;
using AgentShield.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgentShield.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly IScanService _scanService;
        private readonly AuditLogService _auditLogService;

        public ScanController(
            IScanService scanService,
            AuditLogService auditLogService)
        {
            _scanService = scanService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        public async Task<IActionResult> Scan([FromBody] ScanRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return BadRequest("Content cannot be empty.");
            }

            var result = await _scanService.ScanContentWithAiAsync(request.Content);

            // Save scan result to audit log
            var auditLog = new SecurityAuditLog
            {
                Content = request.Content,
                ThreatDetected = result.ThreatDetected,
                AttackTypes = result.AttackTypes,
                Severity = result.Severity,
                Confidence = result.Confidence,
                Action = result.Action,
                Explanation = result.Explanation,

                DetectionMethod = result.ThreatDetected
                    ? "Security Detection Engine"
                    : "AI Threat Detection Agent"
            };

            _auditLogService.AddLog(auditLog);

            return Ok(result);
        }
    }
}