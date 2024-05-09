using CookMaster.Persistance.SqlServer.Extensions;
using CookMaster.Persistence.UOW.Interfaces;
using CookMaster.Persistence.UOW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookMaster.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMsSqlDbContext(configuration);
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
