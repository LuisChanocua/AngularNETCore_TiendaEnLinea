using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DAL.DBContext;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.Models;

namespace Tiendaenlinea.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        public readonly DataBaseContext _dBContext;

        public VentaRepository(DataBaseContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Venta> RegistrarVenta(Venta venta)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dBContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in venta.DetalleVenta)
                    {
                        Producto producto_found = _dBContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        producto_found.Stock = producto_found.Stock - dv.Cantidad;
                        _dBContext.Productos.Update(producto_found);
                    }
                    await _dBContext.SaveChangesAsync();
                    NumeroDocumento correlaritvo = _dBContext.NumeroDocumentos.First();
                    correlaritvo.UltimoNumero = correlaritvo.UltimoNumero + 1;
                    correlaritvo.FechaRegistro = DateTime.Now;

                    _dBContext.NumeroDocumentos.Update(correlaritvo);
                    await _dBContext.SaveChangesAsync();

                    int digitosCant = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", digitosCant));

                    string numeroVenta = ceros + correlaritvo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - digitosCant, digitosCant);

                    venta.NumeroDocumento = numeroVenta;
                    await _dBContext.AddAsync(venta);
                    await _dBContext.SaveChangesAsync();

                    ventaGenerada = venta;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error {ex}");
                    transaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}
