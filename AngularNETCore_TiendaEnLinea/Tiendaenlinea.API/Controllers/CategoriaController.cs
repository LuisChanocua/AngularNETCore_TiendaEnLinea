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
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _categoriaservice;

        public CategoriaController(ICategoriaService service)
        {
            _categoriaservice = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            var rsp = new Response<List<CategoriaDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _categoriaservice.GetCategorias();
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
