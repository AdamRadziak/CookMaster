using AutoMapper;
using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;
using System.Collections;

namespace CookMaster.Aplication.Mappings
{
    public static class Domain2DTOMapper
    {
        public static GetSingleUserDTO MapGetSingleUserDTO(this User domainUser)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, GetSingleUserDTO>());
            var mapper = new Mapper(config);
            GetSingleUserDTO dto = mapper.Map<GetSingleUserDTO>(domainUser);
            return dto;
        }

        public static GetSingleProductDTO MapGetSingleProductDTO(this Product domainProduct)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, GetSingleProductDTO>());
            var mapper = new Mapper(config);
            GetSingleProductDTO dto = mapper.Map<GetSingleProductDTO>(domainProduct);
            return dto;
        }

        public static GetSingleStepDTO MapGetSingleStepDTO(this Step domainStep)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Step, GetSingleStepDTO>());
            var mapper = new Mapper(config);
            GetSingleStepDTO dto = mapper.Map<GetSingleStepDTO>(domainStep);
            return dto;
        }

        public static GetSinglePhotoDTO MapGetSinglePhotoDTO(this Photo domainPhoto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Photo, GetSinglePhotoDTO>());
            var mapper = new Mapper(config);
            GetSinglePhotoDTO dto = mapper.Map<GetSinglePhotoDTO>(domainPhoto);
            return dto;
        }

        public static GetSingleRecipeDTO MapGetSingleRecipeDTO(this Recipe domainRecipe)
        {
            if (domainRecipe == null)
            {
                throw new ArgumentNullException(nameof(domainRecipe));
            }
            //automapper for dto products, steps, photos
            var config_prod = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, GetSingleProductDTO>();
            });
            var mapper_prod = new Mapper(config_prod);
            ICollection<GetSingleProductDTO> dtoProducts = mapper_prod.Map<ICollection<GetSingleProductDTO>>(domainRecipe.Products);

            var config_step = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Step, GetSingleStepDTO>();
            });
            var mapper_step = new Mapper(config_step);
            ICollection<GetSingleStepDTO> dtoSteps = mapper_step.Map<ICollection<GetSingleStepDTO>>(domainRecipe.Steps);

            var config_photo = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, GetSinglePhotoDTO>();
            });
            var mapper_photo = new Mapper(config_photo);
            ICollection<GetSinglePhotoDTO> dtoPhotos = mapper_photo.Map<ICollection<GetSinglePhotoDTO>>(domainRecipe.Photos);

            GetSingleRecipeDTO dto = new()
            {
                Id = domainRecipe.Id,
                Name = domainRecipe.Name,
                Category = domainRecipe.Category,
                IdUser = domainRecipe.IdUser,
                IdMenu = domainRecipe.IdMenu,
                PrepareTime = domainRecipe.PrepareTime,
                MealCount = domainRecipe.MealCount,
                Rate = domainRecipe.Rate,
                Popularity = domainRecipe.Popularity,
                Description = domainRecipe.Description,
                Photos = dtoPhotos,
                Products = dtoProducts,
                Steps = dtoSteps,
            };
            return dto;


        }

        public static GetSingleUserMenuDTO MapGetSingleUserMenuDTO(this UserMenu domainUserMenu)
        {
            if (domainUserMenu == null)
            {
                throw new ArgumentNullException(nameof(domainUserMenu));
            }
            //automapper for dto products, steps, photos
            var config_recipe = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Recipe, GetSingleRecipeDTO>();
            });
            var mapper_recipe = new Mapper(config_recipe);
            ICollection<GetSingleRecipeDTO> dtoRecipes = mapper_recipe.Map<ICollection<GetSingleRecipeDTO>>(domainUserMenu.Recipes);

            GetSingleUserMenuDTO dto = new()
            {
                Id = domainUserMenu.Id,
                Name = domainUserMenu.Name,
                IdUser = domainUserMenu.IdUser,
                Category = domainUserMenu.Category,
                Recipes = dtoRecipes,
            };
            return dto;




        }
    }
}
