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
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var rsp = new Response<DashboardDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _dashboardService.GetDashboard();
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
