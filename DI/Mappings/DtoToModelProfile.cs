using AutoMapper;
using Domain.DTO.Clients.Advice;
using Domain.DTO.Usuario;
using Domain.Models;
using Domain.Models.Clients;

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
            CreateMap<AdviceDto, AdviceModel>().ReverseMap();
            CreateMap<SlipDto, SlipModel>().ReverseMap();
        }
    }
}