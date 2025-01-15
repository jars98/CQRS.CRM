using System.Linq.Expressions;

namespace CQRS.Web.API.Infrastructure.DataAccess.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //Metodo o funcion para obtener un modelo o una tabla Ej. Categoria, Usuario etc.
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro,params Expression<Func<TModel, object>>[] includes);

        //Metodo o funcion para crear un nuevo registro
        Task<TModel> Crear(TModel model);

        //Metodo o funcion para editar un registro
        Task<bool> Editar(TModel model);

        //Metodo o funcion para eliminar un registro
        Task<bool> Eliminar(TModel model);

        //Metodo o funcion para consultar una tabla ya sea con filtro o sin filro
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);
    }
}
