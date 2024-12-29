using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DTO;
using Tiendaenlinea.API.Utilidades;

namespace Tiendaenlinea.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            var rsp = new Response<List<RolDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _rolService.GetRol();
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
