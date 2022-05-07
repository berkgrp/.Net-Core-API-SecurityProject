using EntityLayer;
using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataAccesLayer.IRepository;

namespace DataAccessLayer
{
    public interface IRepositoryRole<T> : IRepository<Role>
    {
        Role GetRoleByID(Int64 roleIDforBitwise, int roleGroupID);
        List<Role> GetRoleListByGroupId(int roleGroupID);
    }
    public class RepositoryRole<T> : Repository<Role>, IRepositoryRole<T>
    {
        public RepositoryRole(ApiDbContext context) : base(context) { }

        public Role GetRoleByID(long roleIDforBitwise, int roleGroupID)
        {
            return ApiDbContext.Roles.Where(r => r.RoleIDForBitwise == roleIDforBitwise && r.RoleGroupID == roleGroupID).FirstOrDefault();
        }

        public List<Role> GetRoleListByGroupId(int roleGroupID)
        {
            return ApiDbContext.Roles.Include(x => x.RoleGroup).Where(x => x.RoleGroupID == roleGroupID).ToList();
        }
    }
}
