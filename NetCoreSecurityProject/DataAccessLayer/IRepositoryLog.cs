using EntityLayer;
using EntityLayer.Models;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryLog<T>:IRepository<Log>
    {
    }
    public class RepositoryLog<T> : Repository<Log>, IRepositoryLog<T>
    {
        public RepositoryLog(ApiDbContext context) : base(context) { }

    }
}
