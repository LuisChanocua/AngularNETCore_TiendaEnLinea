using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Tiendaenlinea.BLL.Servicios.Contrato;
using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.DTO;
using Tiendaenlinea.Models;


namespace Tiendaenlinea.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepository,
            IGenericRepository<MenuRol> menuRolRepository,
            IGenericRepository<Menu> menuRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> GetMenu(int idUsuario)
        {
            IQueryable<Usuario> listaUsuario = await _usuarioRepository.Get(x => x.IdUsuario == idUsuario);
            IQueryable<MenuRol> listamenuRol = await _menuRolRepository.Get();
            IQueryable<Menu> menu = await _menuRepository.Get();

            try
            {
                IQueryable<Menu> listaMenuResultado = (from u in listaUsuario
                                                       join mr in listamenuRol on u.IdRol equals mr.IdRol
                                                       join m in menu on mr.IdMenu equals m.IdMenu
                                                       select m).AsQueryable();

                var listaMenus = listaMenuResultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
