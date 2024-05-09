using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForUser : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<User>(p => p.Email)
                    .CanSort()
                    .CanFilter();

            mapper.Property<User>(p => p.Id)
                .CanSort()
                .CanFilter();

            mapper.Property<User>(p => p.IdMenu)
                .CanSort()
                .CanFilter();
        }
    }
}

