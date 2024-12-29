using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiendaenlinea.API.Utilidades;
using Tiendaenlinea.BLL.Servicios;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DTO;

namespace Tiendaenlinea.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("menu")]
        public async Task<IActionResult> GetList(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _menuService.GetMenu(idUsuario);
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
