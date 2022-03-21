using System;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        IRepositoryUser<T> RepositoryUser { get; }
        int Complete();
    }
}
