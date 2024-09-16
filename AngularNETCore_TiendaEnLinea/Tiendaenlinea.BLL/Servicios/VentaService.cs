using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.DTO;
using Tiendaenlinea.Models;

namespace Tiendaenlinea.BLL.Servicios
{
    internal class VentaService : IVentaService
    {

        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detventaRepository;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepository, IGenericRepository<DetalleVenta> detventaRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _detventaRepository = detventaRepository;
            _mapper = mapper;
        }

        public async Task<VentaDTO> RegistrarVenta(VentaDTO ventaModeloDTO)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.RegistrarVenta(_mapper.Map<Venta>(ventaModeloDTO));
                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se pudo crear la venta");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> HistorialVenta(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepository.Get();
            var listaResultado = new List<Venta>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fechaIn = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                    DateTime fechaFn = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                    listaResultado = await query.Where(f => f.FechaRegistro >= fechaIn && f.FechaRegistro <= fechaFn)
                        .Include(v => v.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
                else
                {
                    listaResultado = await query.Where(f => f.NumeroDocumento == numeroVenta)
                        .Include(v => v.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return _mapper.Map<List<VentaDTO>>(listaResultado);
        }

        public async Task<List<ReporteDTO>> ReporteVenta(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detventaRepository.Get();
            var listaReporte = new List<DetalleVenta>();

            try
            {

                DateTime fechaIn = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime fechaFn = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                listaReporte = await query
                .Include(p => p.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .Where(dv =>
                dv.IdVentaNavigation.FechaRegistro.Value.Date >= fechaIn &&
                dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechaFn).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listaReporte);
        }
    }
}
