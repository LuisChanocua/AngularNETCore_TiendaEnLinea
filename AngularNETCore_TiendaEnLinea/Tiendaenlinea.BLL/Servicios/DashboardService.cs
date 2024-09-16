using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.Models;

using Microsoft.EntityFrameworkCore;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DTO;
using System.Globalization;

namespace Tiendaenlinea.BLL.Servicios
{
    public class DashboardService : IDashboardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public DashboardService(IVentaRepository ventaRepository, IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        //Obtiene los datos de ventas en todo un rango de fechas
        private IQueryable<Venta> ultimasVentasList(IQueryable<Venta> tablaVenta, int menosCantDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).FirstOrDefault();
            ultimaFecha = ultimaFecha.Value.AddDays(menosCantDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> totalVentasUltSemana()
        {
            var totalVentas = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Get();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = ultimasVentasList(_ventaQuery, -7);
                totalVentas = tablaVenta.Count();
            }

            return totalVentas;
        }

        private async Task<string> totalIngresosUltSemana()
        {
            decimal totalIngresos = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Get();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = ultimasVentasList(_ventaQuery, -7);
                totalIngresos = tablaVenta.Select(s => s.Total).Sum(s => s.Value);
            }

            return Convert.ToString(totalIngresos, new CultureInfo("es-MX"));
        }

        private async Task<int> totalProductos()
        {
            IQueryable<Producto> _productoQuery = await _productoRepository.Get();

            return _productoQuery.Count();
        }

        private async Task<Dictionary<string, int>> ventasUltSemana()
        {

            Dictionary<string, int> resultado = new Dictionary<string, int>();
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Get();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = ultimasVentasList(_ventaQuery, -7);

                resultado = tablaVenta.GroupBy(g =>
                g.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                .Select(s => new
                {
                    Fecha = s.Key.ToString("dd/MM/yyyy"),
                    Total = s.Count()
                })
                .ToDictionary(keySelector: d => d.Fecha, elementSelector: d => d.Total);
            }

            return resultado;
        }

        public async Task<DashboardDTO> GetDashboard()
        {
            DashboardDTO vmDashboard = new DashboardDTO();
            try
            {
                vmDashboard.TotalVentas = await totalVentasUltSemana();
                vmDashboard.TotalIngresos = await totalIngresosUltSemana();
                vmDashboard.TotalProductos = await totalProductos();

                List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await ventasUltSemana())
                {
                    listaVentaSemana.Add(
                        new VentaSemanaDTO()
                        {
                            Fecha = item.Key,
                            Total = item.Value
                        });
                }

                vmDashboard.VentaUltimaSemana = listaVentaSemana;

                return vmDashboard;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
