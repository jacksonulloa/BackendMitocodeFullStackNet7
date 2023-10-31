using GamerSellStore.Api.HealthCheck.HealthChecks;
using GamerSellStore.Persistence;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GamerSellStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamerSellStoreDb"));
});

//Añadiendo interfaz visual PI
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "api" })
    .AddDbContextCheck<GamerSellStoreDbContext>(tags: new[] { "database" })
    .AddTypeActivatedCheck<PingHealthCheck>("firebase", HealthStatus.Degraded, 
        tags: new[] { "api" }, args: "firebase.com")
    .AddTypeActivatedCheck<PingHealthCheck>("azure", HealthStatus.Unhealthy, 
        tags: new[] { "api" }, args: "azure.com")
    .AddSqlServer(builder.Configuration.GetConnectionString("GamerSellStoreDb")!, 
        name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] {"database"});

var app = builder.Build();

app.UseHttpsRedirection();

//app.MapHealthChecks("/health", new HealthCheckOptions
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.MapHealthChecks("/health/apis", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = x => x.Tags.Contains("api")
});
app.MapHealthChecks("/health/databases", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate= x=>x.Tags.Contains("database")
});

//Añadiendo interfaz visual PII
app.MapHealthChecksUI();

app.Run();