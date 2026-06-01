using ServerMonitor;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is not configured. Use user-secrets, environment variables, or appsettings.Development.json.");

builder.Services.AddControllers()
    .PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<MonitorService>();
var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapGet("/api/metric", async (MonitorService monitor, AppDbContext db) =>
{
    var metric = new Metric
    {
        RamUsage = monitor.GetRamUsage(),
        CpuInfo = monitor.GetCpuPercent(),
        DiskUsage = monitor.GetDiskUsage(),
        ActiveProcess = monitor.GetProcess(),
        Timestamp = DateTime.UtcNow,
        ServerId = 1
    };

    db.Metrics.Add(metric);

    if (metric.RamUsage > 85)
    {
        db.Alerts.Add(new Alertador
        {
            Message = $"RAM alta: {metric.RamUsage}%",
            Level = "Warning",
            Tipo = "RAM",
            Valor = metric.RamUsage,
            CreatedAt = DateTime.UtcNow
        });
    }

    if (metric.CpuInfo > 90)
    {
        db.Alerts.Add(new Alertador
        {
            Message = $"CPU alta: {metric.CpuInfo}%",
            Level = "Critical",
            Tipo = "CPU",
            Valor = metric.CpuInfo,
            CreatedAt = DateTime.UtcNow
        });
    }

    if (metric.DiskUsage > 80)
    {
        db.Alerts.Add(new Alertador
        {
            Message = $"Disco alto: {metric.DiskUsage}%",
            Level = "Warning",
            Tipo = "Disco",
            Valor = metric.DiskUsage,
            CreatedAt = DateTime.UtcNow
        });
    }

    await db.SaveChangesAsync();
    return Results.Ok(metric);
})
.WithName("GetMetrics")
.WithOpenApi();

app.MapGet("/api/metric/history", async (AppDbContext db) =>
{
    return Results.Ok(await db.Metrics.ToListAsync());
});

app.MapGet("/api/alerts", async (AppDbContext db) =>
{
    return Results.Ok(await db.Alerts
        .OrderByDescending(alert => alert.CreatedAt)
        .ToListAsync());
})
.WithName("GetAlerts")
.WithOpenApi();

app.MapControllers();
app.Run();