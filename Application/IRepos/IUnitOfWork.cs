using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepos
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        Task BeginAsync(CancellationToken ct = default);

        Task CommitAsync(CancellationToken ct = default);

        Task RollbackAsync(CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
