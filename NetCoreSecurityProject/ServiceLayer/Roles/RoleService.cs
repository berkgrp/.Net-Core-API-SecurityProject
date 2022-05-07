using DataAccessLayer;
using EntityLayer.Models;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork<UserRole> _unitOfWorkUserRole;
        private readonly IUnitOfWork<Role> _unitOfWorkRole;
        private readonly IUnitOfWork<User> _unitOfWorkUser;

        public RoleService(IUnitOfWork<UserRole> unitOfWorkUserRole,
            IUnitOfWork<Role> unitOfWorkRole,
            IUnitOfWork<User> unitOfWorkUser)
        {
            _unitOfWorkUserRole = unitOfWorkUserRole;
            _unitOfWorkRole = unitOfWorkRole;
            _unitOfWorkUser = unitOfWorkUser;
        }

        public IServiceResponse<RoleModel> GetRoleById(Guid userID, int roleGroupID, Int64 roleIDforBitwise)
        {
            var response = new ServiceResponse<RoleModel>();
            RoleModel model = new RoleModel();
            var user = _unitOfWorkUser.RepositoryUser.GetUserByGuidID(userID);
            var userRole = _unitOfWorkUserRole.RepositoryUserRole.GetRoleByID(user.UserID, roleGroupID);
            if (userRole != null)
            {
                if (roleIDforBitwise == (userRole.Roles & roleIDforBitwise))
                {
                    var role = _unitOfWorkRole.RepositoryRole.GetRoleByID(roleIDforBitwise,roleGroupID);
                    if (role != null)
                    {
                        model = new RoleModel() 
                        {
                            Id = role.RoleID, 
                            RoleName = role.RoleName,
                            RoleGroupID = (int)userRole.RoleGroupID,
                            RoleID = roleIDforBitwise,
                            UserID = user.UserID,
                            GroupName = userRole.RoleGroup.GroupName
                        };
                    }
                }
                response.Entity = model;
            }
            return response;
        }

        public IServiceResponse<RoleModel> GetRoleListByGroupId(Guid userID, int roleGroupID)
        {
            var response = new ServiceResponse<RoleModel>();
            List<RoleModel> model = new List<RoleModel>();
            var user = _unitOfWorkUser.RepositoryUser.GetUserByGuidID(userID);
            var userRole = _unitOfWorkUserRole.RepositoryUserRole.GetRoleListByGroupId(user.UserID, roleGroupID);
            if (userRole != null)
            {
                var allRoles = _unitOfWorkRole.RepositoryRole.GetRoleListByGroupId(roleGroupID);
                foreach (var role in allRoles)
                {
                    if (role.RoleID == (userRole.Roles & role.RoleID))
                    {
                        model.Add(new RoleModel() 
                        {
                            Id = role.RoleID,
                            RoleName = role.RoleName,
                            RoleGroupID = (int)role.RoleGroupID,
                            RoleID = (int)role.RoleID,
                            UserID = user.UserID,
                            GroupName = role.RoleGroup.GroupName
                        });
                    }
                }
                response.List = model;
            }
            return response;
        }
    }
}
