using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.DTO;
using Tiendaenlinea.Models;

namespace Tiendaenlinea.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> ListaUsuario()
        {
            try
            {

                var queryUsuario = await _usuarioRepository.Get();
                var listaUsuario = queryUsuario.Include(r=> r.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuario);
            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SessionDTO> ValidarSessionUsuario(string correo, string password)
        {
            try
            {
                var queryUsuario = await _usuarioRepository.Get(u => u.Correo == correo && u.Clave == password);
                if(queryUsuario.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("Usuario no encontrado");
                }

                Usuario returnUsuario = queryUsuario.Include(r=> r.IdRolNavigation).First();

                return _mapper.Map<SessionDTO>(returnUsuario);
            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioDTO> CrearUsuario(UsuarioDTO usuarioDTOModel)
        {
            try
            {
                var usuarioCreado = await _usuarioRepository.Create(_mapper.Map<Usuario>(usuarioDTOModel));
                if(usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                var query = await _usuarioRepository.Get(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(r => r.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EditarUsuario(UsuarioDTO usuarioDTOmodel)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(usuarioDTOmodel);

                var usuarioEncontrado = await _usuarioRepository.GetAll(u => u.IdUsuario == usuarioDTOmodel.IdUsuario);

                if(usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("No se pudo encontrar al usuario");
                }

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioRepository.Update(usuarioEncontrado);

                if(!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar al usuario");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EliminarUsuario(int usuarioId)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepository.GetAll(u => u.IdUsuario == usuarioId);

                if(usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("No se pudo encontrar al usuario");
                }

                bool repuesta = await _usuarioRepository.Delete(usuarioEncontrado);

                return repuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }       
       
    }
}
