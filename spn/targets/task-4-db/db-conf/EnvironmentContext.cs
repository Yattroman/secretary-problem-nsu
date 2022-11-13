using Microsoft.EntityFrameworkCore;
using spn.targets.task_4_db.entities;

namespace spn.targets.task_4_db.db_conf;

public sealed class EnvironmentContext : DbContext
{
    public DbSet<SearchLoveTry> SearchLoveTries { get; set; }

    public EnvironmentContext(DbContextOptions<EnvironmentContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchLoveTry>()
            .HasMany(searchLoveTry => searchLoveTry.Contenders)
            .WithOne(contender => contender.SearchLoveTry);

        modelBuilder.Entity<SearchLoveTry>()
            .HasIndex(searchLoveTry => searchLoveTry.Name)
            .IsUnique();
    }
}