using Application.IRepos;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

    public class UnitOfWork(RecoilDbContext db) : IUnitOfWork
    {
        private IDbContextTransaction? _tx;
        private int _depth;
        private bool _aborted;

        public async Task BeginAsync(CancellationToken ct = default)
        {
            if (_depth++ == 0)
            {
                _tx = await db.Database.BeginTransactionAsync(ct);
                _aborted = false;
            }
        }

        public async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
        {
            return await db.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            if (_depth == 0) throw new InvalidOperationException("No active transaction.");

            // An inner scope reporting success does not end the transaction — the outermost one decides.
            if (--_depth > 0) return;

            if (_tx is null) throw new InvalidOperationException("No active transaction.");

            if (_aborted)
            {
                // Committing after an inner scope rolled back would silently persist a partial write.
                await DiscardAsync(ct);
                throw new InvalidOperationException(
                    "The transaction was rolled back by a nested operation and cannot be committed.");
            }

            try { await _tx.CommitAsync(ct); }
            finally { await DisposeTransactionAsync(); }
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if (_depth == 0 || _tx is null) return;

            _aborted = true;

            // Unwind to the outermost scope before touching the database: an inner rollback must undo the
            // whole unit, but the outer scope is still on the stack and will call Commit or Rollback itself.
            if (--_depth > 0) return;

            await DiscardAsync(ct);
        }

        private async Task DiscardAsync(CancellationToken ct)
        {
            if (_tx is null) return;
            try { await _tx.RollbackAsync(ct); }
            finally { await DisposeTransactionAsync(); }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_tx is not null) await _tx.DisposeAsync();
            _tx = null;
            _depth = 0;
            _aborted = false;
        }

        public async ValueTask DisposeAsync()
        {
            if (_tx is not null)
                await DiscardAsync(CancellationToken.None);
        }

        public async Task ExecuteAsync(
    Func<Task> operation,
    CancellationToken ct = default)
        {
            await BeginAsync(ct);

            try
            {
                await operation();

                await SaveChangesAsync(ct);

                await CommitAsync(ct);
            }
            catch
            {
                await RollbackAsync(CancellationToken.None);
                throw;
            }
        }
    }
}
