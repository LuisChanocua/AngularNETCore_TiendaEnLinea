using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tiendaenlinea.API.Utilidades;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DTO;

namespace Tiendaenlinea.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            var rsp = new Response<List<ProductoDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoService.GetProductos();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("saveproduct")]
        public async Task<ActionResult> SaveProduct([FromBody] ProductoDTO productoDTO)
        {
            var rsp = new Response<ProductoDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoService.CrearProducto(productoDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("editproduct")]
        public async Task<ActionResult> EditProduct([FromBody] ProductoDTO productoDTO)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoService.EditarProducto(productoDTO);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("deleteproduct/{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _productoService.EliminarProducto(id);
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
