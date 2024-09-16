using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.Models;

namespace Tiendaenlinea.DAL.Repositorios.Contratos
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<Venta>RegistrarVenta(Venta venta);
    }
}
