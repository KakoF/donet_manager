using Xunit;
using Moq;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Service.Services;
using AutoMapper;
using Domain.DTO.Usuario;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Exceptions;
using Data.Implementations;
using Domain.Interfaces.Implementations;
using IntegratorRabbitMq.Interfaces.RabbitMqIntegrator;

namespace Services.UnitTests
{
    public class GeneroServiceTests
    {
        private readonly GeneroService _sut;


        private readonly MockRepository _mockRepository;
        private readonly Mock<IGeneroImplementation> _mockGeneroRepository;
        IMapper _mapper;

        public GeneroServiceTests()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockGeneroRepository = _mockRepository.Create<IGeneroImplementation>();
            _sut = new GeneroService(_mockGeneroRepository.Object, _mapper);

        }


        [Fact]
        public async void GetAsync_ReturnListGenero()
        {
            //Arr
            var entitys = new List<Genero>()
            {
                new Genero(1, "Genero1", DateTime.Now, null),
                new Genero(2, "Genero2", DateTime.Now, null),
            };
            _mockGeneroRepository.Setup(c => c.ReadAsync()).ReturnsAsync(entitys);

            //Act
            var result = await _sut.ReadAsync();

            //Assert
            _mockGeneroRepository.Verify(c => c.ReadAsync(), Times.Once);
            Assert.Equal(result.Count(), _mapper.Map<List<Genero>>(entitys).Count);
        }

        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Genero, GeneroModel>().ReverseMap();
                cfg.CreateMap<GeneroDto, Genero>().ReverseMap();
                cfg.CreateMap<GeneroDto, GeneroModel>().ReverseMap();

            });
            _mapper = config.CreateMapper();
        }
    }
}
