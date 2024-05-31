using CookMaster.Persistance.SqlServer.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class SieveConfigurationForRecipes : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Recipe>(p => p.Name)
                    .CanSort()
                    .CanFilter();

            mapper.Property<Recipe>(p => p.Id)
                .CanSort()
                .CanFilter();

            mapper.Property<Recipe>(p => p.Popularity)
                .CanSort()
                .CanFilter();

            mapper.Property<Recipe>(p => p.Rate)
                .CanSort()
                .CanFilter();

            mapper.Property<Recipe>(p => p.MealCount)
                .CanSort()
                .CanFilter();

            mapper.Property<Recipe>(p => p.PrepareTime)
                .CanSort()
                .CanFilter();

            mapper.Property<Recipe>(p => p.Products)
                .CanSort()
                .CanFilter();

        }
    }
}
