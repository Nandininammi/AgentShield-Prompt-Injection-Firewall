using AgentShield.Application.Interfaces;
using AgentShield.Application.Services;
using AgentShield.Application.Agents;
using AgentShield.Infrastructure.AI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IScanService, ScanService>();

builder.Services.AddSingleton<AuditLogService>();

builder.Services.AddHttpClient<OllamaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddScoped<IThreatDetectionAgent, ThreatDetectionAgent>();

builder.Services.AddScoped<RiskAssessmentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "http://localhost:57058",
                "http://localhost:59592/",
                 "http://localhost:58926/",
                 "http://localhost:55823/"

            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
