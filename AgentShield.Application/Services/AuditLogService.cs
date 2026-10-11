using AgentShield.Domain.Models;

namespace AgentShield.Application.Services;

public class AuditLogService
{
    private readonly List<SecurityAuditLog> _logs = new();

    public void AddLog(SecurityAuditLog log)
    {
        _logs.Add(log);
    }

    public List<SecurityAuditLog> GetAllLogs()
    {
        return _logs
            .OrderByDescending(x => x.Timestamp)
            .ToList();
    }

    public SecurityAuditLog? GetLogById(Guid id)
    {
        return _logs.FirstOrDefault(x => x.Id == id);
    }
}
