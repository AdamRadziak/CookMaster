using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForPhotos: ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Photo>(p => p.Id)
                    .CanSort()
                    .CanFilter();

            mapper.Property<Photo>(p => p.FileName)
                    .CanSort()
                    .CanFilter();

            mapper.Property<Photo>(p => p.FilePath)
                    .CanSort()
                    .CanFilter();

        }
    }
}
