using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DAL.Repositorios.Contratos;
using Tiendaenlinea.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Tiendaenlinea.DAL.Repositorios
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        public readonly DataBaseContext _dBContext;

        public GenericRepository(DataBaseContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<TModel> GetAll(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel model = await _dBContext.Set<TModel>().FirstOrDefaultAsync(filtro);
                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                throw;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dBContext.Set<TModel>().Add(model);
                var result = await _dBContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                throw;
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dBContext.Set<TModel>().Update(model);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dBContext.Set<TModel>().Remove(model);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Get(Expression<Func<TModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filtro == null ? _dBContext.Set<TModel>() : _dBContext.Set<TModel>().Where(filtro);
                return queryModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error {ex}");
                throw;
            }
        }
    }
}
