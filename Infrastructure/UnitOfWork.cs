using Application.Common.Events;
using Application.IRepos;
using Domain.Events;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{

    public class UnitOfWork(
    RecoilDbContext db,
    IDomainEventDispatcher dispatcher) : IUnitOfWork
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
            if (_depth == 0)
                throw new InvalidOperationException("No active transaction.");

            // Nested scopes do not commit independently.
            if (_depth > 1)
            {
                _depth--;
                return;
            }

            if (_tx is null)
                throw new InvalidOperationException("No active transaction.");

            if (_aborted)
            {
                await DiscardAsync(ct);

                throw new InvalidOperationException(
                    "The transaction was rolled back by a nested operation and cannot be committed.");
            }

            try
            {
                // Dispatch pending domain events.
                await DispatchDomainEventsAsync(ct);

                // Persist changes made by event handlers.
                await db.SaveChangesAsync(ct);

                // Commit the transaction.
                await _tx.CommitAsync(ct);
            }
            catch
            {
                await DiscardAsync(CancellationToken.None);
                throw;
            }
            finally
            {
                if (_tx is not null)
                    await DisposeTransactionAsync();
            }
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

        //Used to find and dispatch events.
        private async Task DispatchDomainEventsAsync(
    CancellationToken ct)
        {
            while (true)
            {
                var entitiesWithEvents = db.ChangeTracker
                    .Entries<Entity>()
                    .Select(entry => entry.Entity)
                    .Where(entity => entity.DomainEvents.Any())
                    .ToList();

                if (entitiesWithEvents.Count == 0)
                    break;

                var domainEvents = entitiesWithEvents
                    .SelectMany(entity => entity.DomainEvents)
                    .ToList();

                await dispatcher.DispatchAsync(domainEvents, ct);

                foreach (var entity in entitiesWithEvents)
                {
                    entity.ClearDomainEvents();
                }
            }
        }
    }
}
