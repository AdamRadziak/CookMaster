using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CookMaster.Persistance.SqlServer.Extensions;

namespace CookMaster.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMsSqlDbContext(configuration);
        }
    }
}
