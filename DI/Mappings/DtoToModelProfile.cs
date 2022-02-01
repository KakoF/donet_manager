using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Models;

namespace DI.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UsuarioDTO, UsuarioModel>().ReverseMap();
            CreateMap<CriarUsuarioDTO, UsuarioModel>().ReverseMap();
            CreateMap<AlterarUsuarioDTO, UsuarioModel>().ReverseMap();
        }
    }
}