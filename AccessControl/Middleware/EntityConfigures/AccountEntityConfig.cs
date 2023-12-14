using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AccessControl.Domain.Aggregates.Account;

namespace AccessControl.Infrastructure.EntityConfigures;

/*
public class AccountEntityConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder) => builder;
        .OwnsMany(acc => acc.Devices);
}
*/