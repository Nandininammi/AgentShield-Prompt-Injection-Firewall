using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentShield.Domain.Models;


namespace AgentShield.Application.Interfaces
{
    public interface IScanService
    {
        ScanResult ScanContent(string content);

        Task<ScanResult> ScanContentWithAiAsync(string content);
    }
}
