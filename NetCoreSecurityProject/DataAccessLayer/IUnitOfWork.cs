using System;
using System.Threading.Tasks;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        IRepositoryUser<T> RepositoryUser { get; }
        IRepositoryRefreshToken<T> RepositoryRefreshToken { get; }
        IRepositoryLog<T> RepositoryLog { get; }
        IRepositoryRole<T> RepositoryRole { get; }
        IRepositoryRoleGroup<T> RepositoryRoleGroup { get; }
        IRepositoryUserRole<T> RepositoryUserRole { get; }
        int Complete();
        Task<int> CompleteAsync();
    }
}
