using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Redis;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.DataConnector;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var entity = await _repository.Get(id);
            if (entity == null)
                return false;
            var delete = await _repository.Delete(id);
            if(!delete)
                throw new DomainException($"Não foi possível excluir o Usuário");

            return delete;

        }

        public async Task<UsuarioDTO> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<UsuarioDTO>(entity);
        }

        public async Task<IEnumerable<UsuarioDTO>> Get()
        {
            IEnumerable<UsuarioDTO> usuariosCache = await _cache.GetListAsync<UsuarioDTO>("Usuarios");
            if (usuariosCache == null)
            {
                var list = await _repository.Get();
                _cache.SetList("Usuarios", list);
                return _mapper.Map<IEnumerable<UsuarioDTO>>(list);
            }

            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuariosCache);
        }

        public async Task<UsuarioDTO> Post(CriarUsuarioDTO data)
        {
            var model = _mapper.Map<UsuarioModel>(data);
            model.Validate();
            var entity = _mapper.Map<Usuario>(model);
            var result = await _repository.Post(entity);
            _cache.Remove("Usuarios");
            _unitOfWork.CommitTransaction();
            return _mapper.Map<UsuarioDTO>(result);
        }

        public async Task<UsuarioDTO> Put(int id, AlterarUsuarioDTO data)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return null;
            var model = _mapper.Map<UsuarioModel>(entity);
            model.Validate();
            _mapper.Map(data, model);
            _mapper.Map(model, entity);
            var result = await _repository.Put(id, entity);
            _cache.Remove("Usuarios");
            return _mapper.Map<UsuarioDTO>(result);
        }
    }
}
