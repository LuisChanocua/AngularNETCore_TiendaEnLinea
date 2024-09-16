using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DTO;

namespace Tiendaenlinea.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<VentaDTO> RegistrarVenta(VentaDTO ventaModeloDTO);
        Task<List<VentaDTO>> HistorialVenta(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<ReporteDTO>> ReporteVenta(string fechaInicio, string fechaFin);
    }
}
