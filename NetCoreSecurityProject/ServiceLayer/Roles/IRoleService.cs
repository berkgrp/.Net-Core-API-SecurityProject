using ServiceLayer.Models;
using System;

namespace ServiceLayer.Roles
{
    public interface IRoleService
    {
        public IServiceResponse<RoleModel> GetRoleById(Guid userGuidID, int roleGroupID, Int64 roleID);
        public IServiceResponse<RoleModel> GetRoleListByGroupId(Guid userGuidID, int roleGroupID);
    }
}
