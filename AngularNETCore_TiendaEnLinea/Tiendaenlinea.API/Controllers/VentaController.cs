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
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        [Route("historial")]
        public async Task<IActionResult> GetHistorial(string buscarpor, string? nventa, string? fInicio, string? fFin)
        {
            var rsp = new Response<List<VentaDTO>>();
            nventa = nventa is null ? "" : nventa;
            fInicio = fInicio is null ? "" : fInicio;
            fFin = fFin is null ? "" : fFin;

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.HistorialVenta(buscarpor, nventa, fInicio, fFin);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet]
        [Route("reporte")]
        public async Task<IActionResult> GetReport(string buscarpor, string? nventa, string? fInicio, string? fFin)
        {
            var rsp = new Response<List<ReporteDTO>>();
            nventa = nventa is null ? "" : nventa;
            fInicio = fInicio is null ? "" : fInicio;
            fFin = fFin is null ? "" : fFin;

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.ReporteVenta( fInicio, fFin);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("saveventa")]
        public async Task<ActionResult> SaveVenta([FromBody] VentaDTO ventaDTO)
        {
            var rsp = new Response<VentaDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _ventaService.RegistrarVenta(ventaDTO);
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
