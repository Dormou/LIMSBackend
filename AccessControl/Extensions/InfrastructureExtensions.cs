using AccessControl.Domain.Aggregates.Account;
using AccessControl.Infrastructure;

namespace AccessControl.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services) => services
        .AddScoped<IAccountRepo, AccountRepo>();
}
