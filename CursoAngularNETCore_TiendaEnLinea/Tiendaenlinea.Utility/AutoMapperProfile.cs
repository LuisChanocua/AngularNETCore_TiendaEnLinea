using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Tiendaenlinea.DTO;
using Tiendaenlinea.Models;

namespace Tiendaenlinea.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region ROL
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region MENU
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region USUARIO
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(d =>
                d.RolDescription,
                opt => opt.MapFrom(o => o.IdRolNavigation.Nombre)
                )

                .ForMember(d => d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0)
                );

            CreateMap<Usuario, SessionDTO>()
                .ForMember(d =>
                d.RolDescription,
                opt => opt.MapFrom(o => o.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(d =>
                d.IdRolNavigation,
                opt => opt.Ignore())

                .ForMember(d =>
                d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false)
                );
            #endregion

            #region CATEGORIA
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region PRODUCTO
            CreateMap<Producto, ProductoDTO>()
                .ForMember(d =>
                d.CategoriaDescription,
                opt => opt.MapFrom(o => o.IdCategoriaNavigation.Nombre))

                .ForMember(d =>
                d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.Precio.Value, new CultureInfo("es-MX")))
                )

                .ForMember(d =>
                d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == true ? 1 : 0));


            CreateMap<ProductoDTO, Producto>()
                .ForMember(d =>
                d.IdCategoriaNavigation,
                opt => opt.Ignore())

                .ForMember(d =>
                d.Precio,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Precio, new CultureInfo("es-MX")))
                )

                .ForMember(d =>
                d.EsActivo,
                opt => opt.MapFrom(o => o.EsActivo == 1 ? true : false));
            #endregion

            #region VENTA
            CreateMap<Venta, VentaDTO>()
                .ForMember(d =>
                d.TotalTexto,
                opt => opt.MapFrom(o => Convert.ToString(o.Total.Value, new CultureInfo("es-MX"))))

                .ForMember(d =>
                d.FechaRegistro,
                opt => opt.MapFrom(o => o.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDTO, Venta>()
                .ForMember(d =>
                d.Total,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.TotalTexto, new CultureInfo("es-MX")))
                );
            #endregion

            #region DETALLE VENTA
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(d =>
                d.ProductoDescription,
                opt => opt.MapFrom(o => o.IdProductoNavigation.Nombre))

                .ForMember(d =>
                d.TotalTexto,
                opt => opt.MapFrom(o => Convert.ToDecimal(o.Total, new CultureInfo("es-MX")))
                );

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(d =>
                d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.PrecioTexto, new CultureInfo("es-MX"))))

                .ForMember(d =>
                d.Total,
                opt => opt.MapFrom(o => Convert.ToString(o.TotalTexto, new CultureInfo("es-MX")))
                );
            #endregion

            #region REPORTE
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(d =>
                d.FechaRegistro,
                opt => opt.MapFrom(o => o.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )

                .ForMember(d =>
                d.NumeroDocumento,
                opt => opt.MapFrom(o => o.IdVentaNavigation.NumeroDocumento))

                .ForMember(d =>
                d.TipoPago,
                opt => opt.MapFrom(o => o.IdVentaNavigation.TipoPago))

                .ForMember(d =>
                d.TotalVenta,
                opt => opt.MapFrom(o => Convert.ToString(o.IdVentaNavigation.Total.Value, new CultureInfo("es-MX"))))

                .ForMember(d =>
                d.Producto,
                opt => opt.MapFrom(o => o.IdProductoNavigation.Nombre))

                .ForMember(d =>
                d.Precio,
                opt => opt.MapFrom(o => Convert.ToString(o.Precio.Value, new CultureInfo("es-MX")))
                )

                .ForMember(d =>
                d.Total,
                opt => opt.MapFrom(o => Convert.ToString(o.Total.Value, new CultureInfo("es-MX")))
                );
            #endregion
        }
    }
}
