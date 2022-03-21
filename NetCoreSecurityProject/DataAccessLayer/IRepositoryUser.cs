using EntityLayer;
using EntityLayer.Models;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryUser<T> : IRepository<User>
    {

    }
    public class RepositoryUser<T> : Repository<User>, IRepositoryUser<T>
    {
        public RepositoryUser(ApiDbContext context) : base(context) { }
    }
}
