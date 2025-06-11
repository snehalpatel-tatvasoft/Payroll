using Microsoft.Extensions.DependencyInjection;
using PalladiumPayroll.Repositories.Home;

namespace PalladiumPayroll.Repositories
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceRepositories(this IServiceCollection services)
        {
            services.AddTransient<IHomeRepository, HomeRepository>();
            return services;
        }
    }
}
