using Microsoft.EntityFrameworkCore;

namespace ServerMonitor;
public class AppDbContext : DbContext
{
    public DbSet<Alertador> Alerts { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Server> Servers{ get; set;}
    public DbSet<Metric> Metrics { get; set;}
    }