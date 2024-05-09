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
    }
}
