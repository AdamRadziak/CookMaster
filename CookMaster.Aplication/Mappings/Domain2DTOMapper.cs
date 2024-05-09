using AutoMapper;
using CookMaster.Aplication.DTOs;
using CookMaster.Persistance.SqlServer.Model;

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


    }
}
