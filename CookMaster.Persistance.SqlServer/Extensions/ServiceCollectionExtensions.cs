using CookMaster.Persistance.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookMaster.Persistance.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMsSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = (configuration.GetConnectionString("CookMasterDatabase"))
                    ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<CookMasterDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
