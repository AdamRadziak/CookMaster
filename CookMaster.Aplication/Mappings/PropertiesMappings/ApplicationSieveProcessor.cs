using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace CookMaster.Aplication.Mappings.PropertiesMappings
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(
            IOptions<SieveOptions> options,
            ISieveCustomSortMethods customSortMethods,
            ISieveCustomFilterMethods customFilterMethods)
            : base(options, customSortMethods, customFilterMethods)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            // create mapper instance of SievePropertyMapper
            var map = mapper.ApplyConfiguration<SieveConfigurationForUser>()
                .ApplyConfiguration<SieveConfigurationForProducts>()
                .ApplyConfiguration<SieveConfigurationForStep>()
                .ApplyConfiguration<SieveConfigurationForPhotos>();

            return map;
        }
    }
}
