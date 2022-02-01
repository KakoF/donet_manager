using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;

namespace DI.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<CriarUsuarioDTO, Usuario>().ReverseMap();
            CreateMap<AlterarUsuarioDTO, Usuario>().ReverseMap();
        }
    }
}