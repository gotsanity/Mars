using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRespository Posts { get; }
        int Complete();
    }
}
