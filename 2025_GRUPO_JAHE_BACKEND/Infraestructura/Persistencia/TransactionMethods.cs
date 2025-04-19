using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructura.Persistencia
{
    public class TransactionMethods : ITransactionMethods
    {
        private readonly ContextoDbSQLServer _context;

        private IDbContextTransaction _transaction;

        public TransactionMethods(ContextoDbSQLServer context)
        {
            _context = context;
            _transaction = null;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction?.RollbackAsync();
            _transaction?.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
