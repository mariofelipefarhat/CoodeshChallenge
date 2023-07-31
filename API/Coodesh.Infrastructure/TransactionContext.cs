using Coodesh.Infrastructure.Entities;
using Coodesh.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Coodesh.Infrastructure;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasConversion(amount => amount / 100, amount => amount * 100);
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        IEnumerable<EntityEntry> entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

        foreach (EntityEntry entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
                ((BaseEntity)entity.Entity).SetCreatedAt(now);
            if (entity.State == EntityState.Modified)
                ((BaseEntity)entity.Entity).SetUpdatedAt(now);
            if (entity.State == EntityState.Deleted)
            {
                ((BaseEntity)entity.Entity).SetDeletedAt(now);
                entity.State = EntityState.Modified;
            }
        }
    }
}
