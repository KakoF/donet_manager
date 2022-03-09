using AutoMapper;
using Data.Implementations;
using Data.Interfaces.DataConnector;

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
        private readonly UsarioImplementation _implementation;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(UsarioImplementation implementation, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _implementation = implementation;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<UsuarioDto> ReadAsync(int id)
        {
            var entity = await _implementation.ReadAsync(id);
            var usuario = _mapper.Map<UsuarioDto>(entity);
            return usuario;
        }

        public async Task<IEnumerable<UsuarioDto>> ReadAsync()
        {
            var list = await _implementation.ReadAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(list);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var delete = await _implementation.DeleteAsync(id);
            return delete;

        }

        public async Task<UsuarioDto> CreateAsync(CriarUsuarioDto data)
        {

            try
            {
                var model = _mapper.Map<UsuarioModel>(data);
                model.Validate();

                var entity = _mapper.Map<Usuario>(model);

                _unitOfWork.BeginTransaction();
                var result = await _implementation.CreateAsync(entity);
                _unitOfWork.CommitTransaction();
                return _mapper.Map<UsuarioDto>(result);
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw new DomainException("Não foi possível cadastrar o usuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            
        }

        public async Task<UsuarioDto> UpdateAsync(int id, AlterarUsuarioDto data)
        {
            var entity = await _implementation.ReadAsync(id);
            if (entity == null)
                return null;

            try
            {
                var model = _mapper.Map<UsuarioModel>(entity);
                _mapper.Map(data, model);
                model.Validate();
                _mapper.Map(model, entity);
                var result = await _implementation.UpdateAsync(id, entity);
                _unitOfWork.CommitTransaction();
                return _mapper.Map<UsuarioDto>(result);
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw new DomainException("Não foi possível alterar o usuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
           
        }
    }
}
