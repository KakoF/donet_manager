using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Models;

namespace DI.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UsuarioDto, UsuarioModel>().ReverseMap();
            CreateMap<CriarUsuarioDto, UsuarioModel>().ReverseMap();
            CreateMap<AlterarUsuarioDto, UsuarioModel>().ReverseMap();
            CreateMap<GeneroDto, GeneroModel>().ReverseMap();
        }
    }
}