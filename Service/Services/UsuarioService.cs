using AutoMapper;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
       
        public UsuarioService(IUsuarioRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<UsuarioDto> ReadAsync(int id)
        {
            var entity = await _repository.ReadAsync(id);
            var usuario = _mapper.Map<UsuarioDto>(entity);
            return usuario;
        }

        public async Task<IEnumerable<UsuarioDto>> ReadAsync()
        {
            var list = await _repository.ReadAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(list);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delete = await _repository.DeleteAsync(id);
            return delete;

        }

        public async Task<UsuarioDto> CreateAsync(CriarUsuarioDto data)
        {
            var model = _mapper.Map<UsuarioModel>(data);
            model.Validate();

            var entity = _mapper.Map<Usuario>(model);
            var result = await _repository.CreateAsync(entity);

            _unitOfWork.CommitTransaction();
            return _mapper.Map<UsuarioDto>(result);
        }

        public async Task<UsuarioDto> UpdateAsync(int id, AlterarUsuarioDto data)
        {
            var entity = await _repository.ReadAsync(id);
            if (entity == null)
                return null;

            var model = _mapper.Map<UsuarioModel>(entity);
            _mapper.Map(data, model);
            model.Validate();
            _mapper.Map(model, entity);
            var result = await _repository.UpdateAsync(id, entity);
            _unitOfWork.CommitTransaction();
            return _mapper.Map<UsuarioDto>(result);
        }
    }
}
