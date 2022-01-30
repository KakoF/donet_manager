using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;
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

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<UsuarioDTO> Get(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<UsuarioDTO>(entity);
        }

        public async Task<IEnumerable<UsuarioDTO>> Get()
        {
            var list = await _repository.Get();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(list);
        }

        public async Task<UsuarioDTO> Post(CriarUsuarioDTO data)
        {
            try
            {
                var model = _mapper.Map<UsuarioModel>(data);
                var entity = _mapper.Map<Usuario>(model);
                var result = await _repository.Post(entity);
                _unitOfWork.CommitTransaction();
                return _mapper.Map<UsuarioDTO>(result);
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Erro");
            }
        }

        public async Task<UsuarioDTO> Put(int id, AlterarUsuarioDTO data)
        {
            var entity = await _repository.Get(id);
            var model = _mapper.Map<UsuarioModel>(entity);
            _mapper.Map(data, model);
            _mapper.Map(model, entity);
            var result = await _repository.Put(id, entity);
            return _mapper.Map<UsuarioDTO>(result);
        }
    }
}
