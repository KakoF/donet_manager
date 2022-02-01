using AutoMapper;
using Domain.Entities;
using Domain.Models;


namespace DI.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Usuario, UsuarioModel>().ReverseMap();
        }
    }
}