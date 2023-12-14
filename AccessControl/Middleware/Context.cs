using Microsoft.EntityFrameworkCore;

//using MediatR;
//using Dotseed.Context;
//Возможно это понадобится

using AccessControl.Domain.Aggregates.Account;
using Dotseed.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccessControl.Infrastructure;

public class Context : DbContext, IUnitOfWork, IDisposable
{
    public DbSet<Account> Accounts { get; set; }

    public Context(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfiguration(new AccountEntityConfig());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);

        return true;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        List<EntityEntry> modifiedEntries = (from x in ChangeTracker.Entries()
                                             where x.State == EntityState.Added || x.State == EntityState.Modified
                                             select x).ToList();

        foreach (EntityEntry entry in modifiedEntries)
        {
            Entity entity = entry.Entity as Entity;
            DateTime now = DateTime.UtcNow;
            if (entity != null && entry.State == EntityState.Added)
            {
                entity.SetCreatedAt(now);
            }

            if (entity != null)
            {
                entity.SetUpdateAt(now);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}