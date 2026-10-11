using AgentShield.Domain.Models;

namespace AgentShield.Application.Services;

public class RiskAssessmentService
{
    public ScanResult Assess(ScanResult result)
    {
        if (!result.ThreatDetected)
        {
            // AI said no threat.
            // Very low confidence should not automatically be trusted.
            if (result.Confidence < 0.50)
            {
                result.Severity = "Medium";
                result.Action = "Review";
                result.Explanation =
                    "No threat was detected, but the AI confidence is low. " +
                    "Human review is recommended.";

                return result;
            }

            result.Severity = "Low";
            result.Action = "Allow";

            return result;
        }

        // Critical attack categories
        bool criticalAttack =
            result.AttackTypes.Contains("Secret Extraction") ||
            result.AttackTypes.Contains("Credential Theft") ||
            result.AttackTypes.Contains("Tool Abuse");

        if (criticalAttack && result.Confidence >= 0.80)
        {
            result.Severity = "Critical";
            result.Action = "Block";

            return result;
        }

        // Multiple attack types with high confidence
        if (result.AttackTypes.Count >= 2 &&
            result.Confidence >= 0.80)
        {
            result.Severity = "High";
            result.Action = "Block";

            return result;
        }

        // Single detected threat with good confidence
        if (result.Confidence >= 0.80)
        {
            result.Severity = "High";
            result.Action = "Block";

            return result;
        }

        // Threat detected but confidence is uncertain
        result.Severity = "Medium";
        result.Action = "Review";

        result.Explanation =
            "A potential threat was detected, but the AI confidence " +
            "is insufficient for automatic blocking. Human review is recommended.";

        return result;
    }
}