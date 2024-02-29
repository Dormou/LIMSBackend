using testing_managment.Interfaces;
using testing_managment.Repository;

namespace testing_managment.ApplicationServices
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IRequestsRepository, RequestsRepository>();
            return services;
        }
    }
}
