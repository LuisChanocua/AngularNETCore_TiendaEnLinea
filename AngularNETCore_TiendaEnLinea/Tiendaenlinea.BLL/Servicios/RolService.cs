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
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> GetRol()
        {
            try
            {
                var listaRoles = await _rolRepository.Get();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
