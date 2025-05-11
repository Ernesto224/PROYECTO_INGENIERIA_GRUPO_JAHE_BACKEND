using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Nucleo
{
    public interface IBaseRepositorio<TEntity> where TEntity : class
    {
        public Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class;
        public Task CrearAsync<T>(T entity) where T : class;
        public Task UpdateAsync<T>(T entity) where T : class;
        public Task DeleteAsync<T>(T entity) where T : class;
    }
}
