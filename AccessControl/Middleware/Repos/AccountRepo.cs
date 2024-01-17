using Microsoft.EntityFrameworkCore;

using Dotseed.Domain;

using AccessControl.Domain.Aggregates.Account;

namespace AccessControl.Infrastructure;

public class AccountRepo : IAccountRepo
{
    public readonly Context _db;

    public AccountRepo(Context db) => _db = db;

    public IUnitOfWork UnitOfWork => _db;

    public async Task AddAsync(Account account) => await _db.Accounts.AddAsync(account);

    public async Task<Account> FindByActivationCodeAsync(string ActivationCode) => await _db.Accounts.FirstOrDefaultAsync(acc => acc.ActivationCode == ActivationCode);

    public async Task<Account> FindByEmailAsync(string Email) => await _db.Accounts.FirstOrDefaultAsync(acc => acc.Email == Email);

    public Task<Account> FindByIdAsync(Guid Id) => _db.Accounts.FirstOrDefaultAsync(acc => acc.Id == Id);

    public async Task RemoveById(Guid Id) => _db.Accounts.Remove(await _db.Accounts
        .FirstOrDefaultAsync(acc => acc.Id == Id)
        ?? throw new());
}
