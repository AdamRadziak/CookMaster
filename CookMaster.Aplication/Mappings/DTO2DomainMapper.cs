using AutoMapper;
using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using CookMaster.Persistence.UOW.Interfaces;

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

        public static Photo MapPhoto(this AddUpdatePhotoDTO dto, byte[]? Data)
        {
            if (dto == null)
            { throw new ArgumentNullException(nameof(dto)); }
            Photo domainPhoto = new()
            {
                IdRecipe = dto.IdRecipe,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                Data = Data

            };
            return domainPhoto;
        }

        public static Recipe MapRecipe(this AddUpdateRecipeDTO dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AddUpdateRecipeDTO, Recipe>());
            var mapper = new Mapper(config);
            Recipe recipe = mapper.Map<Recipe>(dto);
            return recipe;
        }

        public static UserMenu MapUserMenu(this AddUpdateUserMenuDTO dto, ICollection<Recipe> recipes)
        {
            if (dto == null)
            { throw new ArgumentNullException(nameof(dto)); }

            UserMenu domainUserMenu = new()
            {
                Name = dto.Name,
                IdUser = dto.IdUser,
                Category = dto.Category,
                Recipes = recipes,
            };

            return domainUserMenu;

        }

        public static UserMenu GenerateUserMenuMaping(this GenerateUserMenuDTO dto, ICollection<Recipe> recipes)
        {
            if (dto == null)
            { throw new ArgumentNullException(nameof(dto)); }
            UserMenu domainUserMenu = new()
            {
                Name = dto.Name,
                IdUser = dto.IdUser,
                Category = dto.Category,
                Recipes = recipes
            };
            return domainUserMenu;
        }
    }
}
