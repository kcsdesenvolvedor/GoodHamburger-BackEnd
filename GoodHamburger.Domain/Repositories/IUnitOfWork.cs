using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
