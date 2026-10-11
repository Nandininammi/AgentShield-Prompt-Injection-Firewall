AgentShield – Prompt Injection Firewall

About the Project

I developed AgentShield as a security layer for AI applications to identify potentially malicious prompts before they can influence an AI assistant.

The application combines a rule-based detection mechanism with an AI-based threat analysis agent. The rule engine checks incoming content for known attack patterns. When no rule matches, the application can use an Ollama-hosted language model to analyze the content further.

I built the backend using C# and ASP.NET Core Web API and the frontend using Angular. The application also includes a Dashboard for viewing threat statistics and an Audit Logs page for reviewing recorded scan activity.

What I Implemented

Developed a scan API that accepts user-provided content and returns a threat analysis result.

Implemented rule-based checks for instruction override, role change, secret extraction, credential theft, tool abuse, context poisoning, and indirect prompt injection.

Integrated Ollama with the Llama 3.2 model for additional AI-based threat analysis.

Added risk severity, confidence, explanation, and recommended security action to scan results.

Built an Angular interface to submit prompts and display the scan results.

Added a Dashboard to view scan statistics and attack categories.

Implemented an Audit Logs view to review scan activity.

How the Application Works

A user submits content through the Angular Scan page.

The ASP.NET Core API receives the request.

The scan service checks the content against predefined threat patterns.

If the rule engine does not identify a threat, the AI threat detection agent can analyze the content using Ollama.

The service returns the scan result, including the threat classification, severity, confidence, and action.

The Angular interface displays the result, and scan activity can be reviewed through the application's audit functionality.

Testing

I tested the application with normal questions and malicious prompt examples. Normal requests such as asking about office hours were allowed, while tested prompt injection and secret extraction attempts were blocked.

These tests helped me verify the main scan workflow and check that ordinary questions were not incorrectly classified as attacks.

Technology Stack

C#

ASP.NET Core Web API

Angular and TypeScript

Ollama

Llama 3.2

Git and GitHub

Limitations

AgentShield is currently a prototype. Rule-based checks and AI-based classification can produce false positives or miss some attacks. Further testing is needed to evaluate detection accuracy across a wider range of adversarial inputs before production use.

Source Code

GitHub: https://github.com/Nandininammi/AgentShield-Prompt-Injection-Firewall
