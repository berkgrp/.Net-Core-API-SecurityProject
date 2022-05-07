using EntityLayer;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryUserRole<T> : IRepository<UserRole>
    {

        UserRole GetRoleByID(int userId, int roleGroupID);//Asla ve asla değiştirilmemeli !
        UserRole GetRoleListByGroupId(int userId, int roleGroupID);//Asla ve asla değiştirilmemeli !
    }
    public class RepositoryUserRole<T> : Repository<UserRole>, IRepositoryUserRole<T>
    {
        public RepositoryUserRole(ApiDbContext context) : base(context) { }

        public UserRole GetRoleByID(int userId, int roleGroupID)
        {
            return ApiDbContext.UserRole.Include(r => r.RoleGroup)
                .FirstOrDefault(ur => ur.UserID == userId && ur.RoleGroupID == roleGroupID);
        }

        public UserRole GetRoleListByGroupId(int userId, int roleGroupID)
        {
            return ApiDbContext.UserRole.FirstOrDefault(ur => ur.UserID == userId && ur.RoleGroupID == roleGroupID);
        }
    }
}
