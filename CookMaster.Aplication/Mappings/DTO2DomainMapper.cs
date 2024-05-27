using AutoMapper;
using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;

namespace CookMaster.Aplication.Mappings
{
    public static class DTO2DomainMapper
    {
        public static User MapUser(this AddUpdateUserDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateUserDTO, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(dto);
            return user;
        }

        public static Product MapProduct(this AddUpdateProductDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateProductDTO, Product>());
            var mapper = new Mapper(config);
            Product product = mapper.Map<Product>(dto);
            return product;
        }

        public static Step MapStep(this AddUpdateStepDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateStepDTO, Step>());
            var mapper = new Mapper(config);
            Step step = mapper.Map<Step>(dto);
            return step;
        }

        public static Photo MapPhoto(this AddUpdatePhotoDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdatePhotoDTO, Photo>());
            var mapper = new Mapper(config);
            Photo photo = mapper.Map<Photo>(dto);
            return photo;
        }

        public static Recipe MapRecipe(this AddUpdateRecipeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateRecipeDTO, Recipe>());
            var mapper = new Mapper(config);
            Recipe recipe = mapper.Map<Recipe>(dto);
            return recipe;
        }
    }
}
