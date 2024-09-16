using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DTO;

namespace Tiendaenlinea.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetUsuario();
        Task<SessionDTO> ValidarSessionUsuario(string correo, string password);
        Task<UsuarioDTO> CrearUsuario(UsuarioDTO usuarioDTOModel);
        Task<bool> EditarUsuario(UsuarioDTO usuarioDTOmodel);
        Task<bool> EliminarUsuario(int usuarioId);
    }
}
