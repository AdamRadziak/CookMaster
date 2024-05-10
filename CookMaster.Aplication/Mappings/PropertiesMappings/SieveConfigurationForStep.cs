using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForStep : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Step>(p => p.Id)
                    .CanSort()
                    .CanFilter();

        }
    
    }
}
