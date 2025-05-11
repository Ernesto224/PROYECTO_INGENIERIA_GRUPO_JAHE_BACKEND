using System.Linq.Expressions;
using Dominio.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Nucleo
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepositorio(DbContext context)
        {
            this._context = context;
        }

        public async Task CrearAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
        }

        public Task DeleteAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);

            return Task.CompletedTask;
        }
    }
}
