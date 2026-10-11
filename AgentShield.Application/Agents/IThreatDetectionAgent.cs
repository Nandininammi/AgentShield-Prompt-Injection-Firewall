using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentShield.Domain.Models;

namespace AgentShield.Application.Agents
{
    public interface IThreatDetectionAgent
    {
        Task<ThreatAnalysisResult> AnalyzeAsync(string content);
    }
}
