using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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