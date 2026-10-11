using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentShield.Domain.Models;

public class SecurityAuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string Content { get; set; } = string.Empty;

    public bool ThreatDetected { get; set; }

    public List<string> AttackTypes { get; set; } = new();

    public string Severity { get; set; } = "Low";

    public double Confidence { get; set; }

    public string Action { get; set; } = "Allow";

    public string DetectionMethod { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;
}
