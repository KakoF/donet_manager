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
        private readonly IRedisIntegrator _cache;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IRedisIntegrator cache)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<UsuarioDTO> ReadAsync(int id)
        {
            UsuarioDTO usuarioCache = await _cache.GetAsync<UsuarioDTO>($"Usuario_{id}");
            if (usuarioCache == null)
            {
                var entity = await _repository.GetAsync(id);
                var usuario = _mapper.Map<UsuarioDTO>(entity);
                _cache.Set($"Usuario_{id}", usuario);
                return usuario;
            }
            return _mapper.Map<UsuarioDTO>(usuarioCache);
        }

        public async Task<IEnumerable<UsuarioDTO>> ReadAsync()
        {
            var list = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(list);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delete = await _repository.DeleteAsync(id);
            return delete;

        }

        public async Task<UsuarioDTO> CreateAsync(CriarUsuarioDTO data)
        {
            var model = _mapper.Map<UsuarioModel>(data);
            model.Validate();
            var entity = _mapper.Map<Usuario>(model);
            var result = await _repository.PostAsync(entity);
            _unitOfWork.CommitTransaction();
            return _mapper.Map<UsuarioDTO>(result);
        }

        public async Task<UsuarioDTO> UpdateAsync(int id, AlterarUsuarioDTO data)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return null;

            var model = _mapper.Map<UsuarioModel>(entity);
            _mapper.Map(data, model);
            model.Validate();
            _mapper.Map(model, entity);
            var result = await _repository.PutAsync(id, entity);
            _cache.Remove($"Usuario_{id}");
            _unitOfWork.CommitTransaction();
            return _mapper.Map<UsuarioDTO>(result);
        }
    }
}
