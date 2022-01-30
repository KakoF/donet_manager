using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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