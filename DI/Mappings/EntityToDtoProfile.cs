using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;

namespace DI.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<CriarUsuarioDto, Usuario>().ReverseMap();
            CreateMap<AlterarUsuarioDto, Usuario>().ReverseMap();
            CreateMap<GeneroDto, Genero>().ReverseMap();
        }
    }
}