using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForProducts : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Product>(p => p.Name)
                    .CanSort()
                    .CanFilter();

            mapper.Property<Product>(p => p.Id)
                .CanSort()
                .CanFilter();
            
            mapper.Property<Product>(p => p.Category)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.Amount)
                .CanSort()
                .CanFilter();
        }
    }
}
