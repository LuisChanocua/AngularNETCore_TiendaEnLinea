using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DAL.Repositorios.Contratos;

namespace Tiendaenlinea.DAL.Repositorios.Contratos
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //Obtener
        Task<TModel> GetAll(Expression<Func<TModel, bool>> filtro);

        Task<TModel> Create(TModel model);

        Task<bool> Update(TModel model);

        Task<bool> Delete(TModel model);

        //Consultar
        Task<IQueryable<TModel>> Get(Expression<Func<TModel, bool>> filtro = null);
    }
}
