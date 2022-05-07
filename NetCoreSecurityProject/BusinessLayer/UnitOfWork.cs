using DataAccessLayer;
using EntityLayer;
using System;
using System.Threading.Tasks;
using static DataAccesLayer.IRepository;

namespace BusinessLayer
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ApiDbContext _context;
        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
            Repository = new Repository<T>(_context);
            RepositoryUser = new RepositoryUser<T>(_context);
            RepositoryLog = new RepositoryLog<T>(_context);
            RepositoryRefreshToken = new RepositoryRefreshToken<T>(_context);
            RepositoryRole = new RepositoryRole<T>(_context);
            RepositoryRoleGroup = new RepositoryRoleGroup<T>(_context);
            RepositoryUserRole = new RepositoryUserRole<T>(_context);
        }
        public IRepository<T> Repository { get; private set; }
        public IRepositoryUser<T> RepositoryUser { get; private set; }
        public IRepositoryLog<T> RepositoryLog { get; private set; }
        public IRepositoryRefreshToken<T> RepositoryRefreshToken { get; private set; }
        public IRepositoryRole<T> RepositoryRole { get; private set; }
        public IRepositoryRoleGroup<T> RepositoryRoleGroup { get; private set; }
        public IRepositoryUserRole<T> RepositoryUserRole { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
