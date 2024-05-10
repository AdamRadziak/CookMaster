using CookMaster.Aplication.CustomQueries;
using CookMaster.Aplication.DTOs;
using CookMaster.Aplication.Mappings.PropertiesMappings;
using CookMaster.Aplication.Services;
using CookMaster.Aplication.Services.Interfaces;
using CookMaster.Aplication.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sieve.Models;
using Sieve.Services;


namespace CookMaster.Aplication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SieveOptions>(sieveOptions =>
            {
                configuration.GetSection("Sieve").Bind(sieveOptions);
            });
            services
                .AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>()
                .AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>()
                .AddScoped<ISieveProcessor, ApplicationSieveProcessor>()
                .AddScoped(typeof(IUserService), typeof(UserService))
                .AddScoped(typeof(IProductService), typeof(ProductService))
                .AddScoped(typeof(IStepService), typeof(StepService))
                .AddScoped(typeof(IPhotoService), typeof(PhotoService));

            services.AddScoped<IValidator<AddUpdateUserDTO>, AddUpdateUserDTOValidator>();
            // services.AddScoped<IValidator<AddUpdateProductDTO>, AddUpdateProductDTOValidator>();
        }
    }
}
