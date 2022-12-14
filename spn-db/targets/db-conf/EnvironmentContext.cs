using Microsoft.EntityFrameworkCore;
using spn_db.targets.entities;

namespace spn_db.targets.db_conf;

public sealed class EnvironmentContext : DbContext
{
    public DbSet<SearchLoveTryEntity> SearchLoveTries { get; set; }
    public DbSet<IterationEntity> IterationEntities { get; set; }

    public EnvironmentContext(DbContextOptions<EnvironmentContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchLoveTryEntity>()
            .HasMany(searchLoveTry => searchLoveTry.Contenders)
            .WithOne(contender => contender.SearchLoveTry);

        modelBuilder.Entity<SearchLoveTryEntity>()
            .HasIndex(searchLoveTry => searchLoveTry.Name)
            .IsUnique();

        modelBuilder.Entity<IterationEntity>()
            .HasOne(iterationEntity => iterationEntity.SearchLoveTry);

        modelBuilder.Entity<IterationEntity>()
            .HasIndex(iterationEntity => iterationEntity.SearchLoveTryId)
            .IsUnique();
    }
}