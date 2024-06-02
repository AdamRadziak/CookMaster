using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForUserMenu : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<UserMenu>(p => p.Name)
                    .CanSort()
                    .CanFilter();

            mapper.Property<UserMenu>(p => p.Category)
                    .CanSort()
                    .CanFilter();

            mapper.Property<UserMenu>(p => p.Recipes)
                    .CanSort()
                    .CanFilter();

            mapper.Property<UserMenu>(p => p.IdUserNavigation.Email)
                    .CanSort()
                    .CanFilter();

        }
    }
}
