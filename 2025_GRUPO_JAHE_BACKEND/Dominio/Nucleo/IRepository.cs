using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Nucleo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        Task CrearAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;

    }
}
