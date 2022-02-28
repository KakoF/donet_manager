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

        public async Task<bool> Delete(int id)
        {
            //await Find(id);
            var delete = await _repository.Delete(id);
            return delete;

        }

        public async Task<UsuarioDTO> Get(int id)
        {
            UsuarioDTO usuarioCache = await _cache.GetAsync<UsuarioDTO>($"Usuario_{id}");
            if (usuarioCache == null)
            {
                var entity = await Find(id);
                var usuario = _mapper.Map<UsuarioDTO>(entity);
                _cache.Set($"Usuario_{id}", usuario);
                return usuario;
            }
            return _mapper.Map<UsuarioDTO>(usuarioCache);
        }

        public async Task<IEnumerable<UsuarioDTO>> Get()
        {
            var list = await _repository.Get();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(list);
        }

        public async Task<UsuarioDTO> Post(CriarUsuarioDTO data)
        {
            var model = _mapper.Map<UsuarioModel>(data);
            model.Validate();
            var entity = _mapper.Map<Usuario>(model);
            var result = await _repository.Post(entity);
            _unitOfWork.CommitTransaction();
            _cache.Remove($"Usuario_{result.Id}");
            return _mapper.Map<UsuarioDTO>(result);
        }

        public async Task<UsuarioDTO> Put(int id, AlterarUsuarioDTO data)
        {
            var entity = await Find(id);
            if(entity == null)
                return null;

            var model = _mapper.Map<UsuarioModel>(entity);
            model.Validate();
            _mapper.Map(data, model);
            _mapper.Map(model, entity);
            var result = await _repository.Put(id, entity);
            _cache.Remove($"Usuario_{id}");
            return _mapper.Map<UsuarioDTO>(result);
        }

        private async Task<Usuario> Find(int id)
        {
            var entity = _mapper.Map<Usuario>(await _repository.Get(id));
            return entity;
        }
    }
}
