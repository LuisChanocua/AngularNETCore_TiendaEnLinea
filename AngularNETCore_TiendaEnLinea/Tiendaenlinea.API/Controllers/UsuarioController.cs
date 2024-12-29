using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Tiendaenlinea.API.Controllers;
using Tiendaenlinea.API.Utilidades;
using Tiendaenlinea.BLL.Servicios;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DTO;

namespace Tiendaenlinea.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            var rsp = new Response<List<UsuarioDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.GetUsuario();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var rsp = new Response<SessionDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.ValidarSessionUsuario(loginDTO.Correo, loginDTO.Password);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("saveuser")]
        public async Task<ActionResult> SaveUser([FromBody] UsuarioDTO usuarioDTO)
        {
            var rsp = new Response<UsuarioDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.CrearUsuario(usuarioDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("edituser")]
        public async Task<ActionResult> EditUser([FromBody] UsuarioDTO usuarioDTO)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.EditarUsuario(usuarioDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("deleteuser/{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _usuarioService.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
