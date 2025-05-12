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

        public virtual async Task CrearAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Remove(entity);

            return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entidades = await this._context.Set<TEntity>().ToListAsync();

            return entidades;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<TEntity>().Update(entity);

            return Task.CompletedTask;
        }
    }
}
