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
    }
}
