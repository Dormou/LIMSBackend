using Microsoft.EntityFrameworkCore;
using testing_managment.Data;

namespace testing_managment.ApplicationServices
{
    public static class DbSevice
    {
        //Метод добавления сервисов БД
        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContextTests>(opts =>
            {
                opts.UseNpgsql(configuration.GetConnectionString("Database"));
                opts.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}
