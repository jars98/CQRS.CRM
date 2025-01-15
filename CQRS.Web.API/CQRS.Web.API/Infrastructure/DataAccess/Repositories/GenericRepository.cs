using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRS.Web.API.Infrastructure.DataAccess.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly ApplicationDbContextPostgresSQL _dbcontext;

        public GenericRepository(ApplicationDbContextPostgresSQL dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro, params Expression<Func<TModel, object>>[] includes)
        {
            try
            {
                IQueryable<TModel> query = _dbcontext.Set<TModel>();

                // Obtenemos las tablas FK
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                return await query.FirstOrDefaultAsync(filtro);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TModel> Crear(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Add(model);
                await _dbcontext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Update(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Remove(model);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filtro == null ? _dbcontext.Set<TModel>() : _dbcontext.Set<TModel>().Where(filtro);
                return queryModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
