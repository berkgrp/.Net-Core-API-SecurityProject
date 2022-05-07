using EntityLayer;
using EntityLayer.Models;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryRoleGroup<T> : IRepository<RoleGroup>
    {
    }
    public class RepositoryRoleGroup<T> : Repository<RoleGroup>, IRepositoryRoleGroup<T>
    {
        public RepositoryRoleGroup(ApiDbContext context) : base(context) { }
    }
}
