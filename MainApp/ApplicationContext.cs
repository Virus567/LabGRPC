using Microsoft.EntityFrameworkCore;
using LabDB.Entity;

namespace MainApp;

public class ApplicationContext : DbContext
{
    private readonly bool _test;
    public DbSet<Agent> Agents { get; set; } = null!;
    public DbSet<Computer> Computers { get; set; } = null!;
    public DbSet<LoadedApp> LoadedApps { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        _test = false;
        Database.Migrate();
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, bool test) : base(options)
    {
        _test = test;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (_test)
        {
            base.OnModelCreating(modelBuilder);
            return;
        }
        var agent = new Agent {Id = 1, Login = "Test", Password = "Test"};
        var computer = new Computer {Id = 1};
        
        var inds = new List<object>(new[]
        {
            new {Id = 1, AgentId = 1, ComputerId = 1,  Name = "3D max", DateTime = DateTime.Now},
            new {Id = 2, AgentId = 1, ComputerId = 1,  Name = "AutoCAD", DateTime = DateTime.Now},
            new {Id = 3, AgentId = 1, ComputerId = 1,  Name = "Проводник", DateTime = DateTime.Now},
            new {Id = 4, AgentId = 1, ComputerId = 1,  Name = "Visual Studio 2022", DateTime = DateTime.Now},
        });
        
        modelBuilder.Entity<Agent>().HasData(agent);
        modelBuilder.Entity<Computer>().HasData(computer);
        modelBuilder.Entity<LoadedApp>(i =>
        {
            i.HasOne(ind => ind.Agent)
                .WithMany(e => e.LoadedApps)
                .HasForeignKey(ind => ind.AgentId);
            i.HasOne(ind => ind.Computer)
                .WithMany(e => e.LoadedApps)
                .HasForeignKey(ind => ind.ComputerId);
            i.HasData(inds);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}