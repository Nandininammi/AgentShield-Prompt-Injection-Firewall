using AgentShield.Application.Agents;
using Microsoft.AspNetCore.Mvc;

namespace AgentShield.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentController : ControllerBase
{
    private readonly IThreatDetectionAgent _agent;

    public AgentController(IThreatDetectionAgent agent)
    {
        _agent = agent;
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze(
        [FromBody] AgentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest("Content cannot be empty.");
        }

        var result =
            await _agent.AnalyzeAsync(request.Content);

        return Ok(result);
    }
}

public class AgentRequest
{
    public string Content { get; set; } = string.Empty;
}
