using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.DTO;
using Tiendaenlinea.Models;

namespace Tiendaenlinea.BLL.Servicios
{
    public class ProductoService : IProducto
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> GetProductos()
        {
            try
            {
                var queryProductos = await _productoRepository.Get();
                var listarProductos = queryProductos.Include(i => i.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listarProductos.ToList());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductoDTO> CrearProducto(ProductoDTO productoDTOModel)
        {
            try
            {
                var productoCreado = await _productoRepository.Create(_mapper.Map<Producto>(productoDTOModel));
                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EditarProducto(ProductoDTO productoDTOModel)
        {
            try
            {
                var productoModel = _mapper.Map<Producto>(productoDTOModel);
                var productoEncontrado = await _productoRepository.GetAll(p => p.IdProducto == productoModel.IdProducto);

                if(productoEncontrado.IdProducto == 0) throw new TaskCanceledException("No se pudo encontrar el producto");

                productoEncontrado.Nombre = productoModel.Nombre;
                productoEncontrado.IdCategoria = productoModel.IdCategoria;
                productoEncontrado.Stock = productoModel.Stock;
                productoEncontrado.Precio = productoModel.Precio;
                productoEncontrado.EsActivo = productoModel.EsActivo;

                bool respuesta = await _productoRepository.Update(productoEncontrado);
                
                if (!respuesta) throw new TaskCanceledException("No se puedo editar el producto");
                
                return respuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> EliminarProducto(int productoId)
        {
            try
            {
                var productoEncontrado = await _productoRepository.GetAll(p => p.IdProducto == productoId);

                if (productoEncontrado == null) 
                    throw new TaskCanceledException("No se pudo encontrar el producto");

                bool respuesta = await _productoRepository.Delete(productoEncontrado);
                if (!respuesta) throw new TaskCanceledException("No se pudo eliminar el producto");

                return respuesta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
