using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DTO;

namespace Tiendaenlinea.BLL.Servicios.Contrato
{
    public interface IProducto
    {
        Task<List<ProductoDTO>> GetProductos();
        Task<ProductoDTO> CrearProducto(ProductoDTO productoDTOModel);
        Task<bool> EditarProducto(ProductoDTO productoDTOModel);
        Task<bool> EliminarProducto(int productoId);
    }
}
