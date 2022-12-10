using Microsoft.EntityFrameworkCore;
using LabDB.Entity;
using Microsoft.EntityFrameworkCore;

namespace MainApp;

public class ApplicationContext : DbContext
{
    public DbSet<Agent> Agents { get; set; } = null!;
    public DbSet<Computer> Computers { get; set; } = null!;
    public DbSet<LoadedApp> LoadedApps { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}