using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentShield.Domain.Models
{
    public class ThreatAnalysisResult
    {
        public bool ThreatDetected { get; set; }

        public List<string> AttackTypes { get; set; } = new();

        public string Severity { get; set; } = "Low";

        public double Confidence { get; set; }

        public string RecommendedAction { get; set; } = "Allow";

        public string Explanation { get; set; } = string.Empty;
    }
}
