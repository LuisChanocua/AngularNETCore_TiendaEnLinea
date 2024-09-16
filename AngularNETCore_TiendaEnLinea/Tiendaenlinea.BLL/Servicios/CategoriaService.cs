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
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            try
            {
                var listaCategorias = await _categoriaRepository.Get();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());

            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
