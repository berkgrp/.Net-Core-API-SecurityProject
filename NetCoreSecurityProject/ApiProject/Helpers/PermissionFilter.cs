using DataAccessLayer;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ApiProject.Helpers
{
    public class PermissionFilter
    {
        private readonly IUnitOfWork<User> _unitOfWorkUser;

        public PermissionFilter(IUnitOfWork<User> unitOfWorkUser)
        {
            _unitOfWorkUser = unitOfWorkUser;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _ = int.TryParse(context.HttpContext.Request.Headers["UserId"].FirstOrDefault(), out int userId);
            string actionName = context.ActionDescriptor.RouteValues["action"];
            if (userId != 0 && !HasValidRole(userId, actionName))
            {
                context.Result = new ObjectResult(context.ModelState)
                {
                    Value = "You are not authorized for this page, talk with the admin for the role attributes.",
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden
                };
                return;
            }
        }
        public bool HasValidRole(int userId, string actionName)
        {
            var user = _unitOfWorkUser.RepositoryUser.IsUserHasThatRole(userId);
            if (user != null)
            {
                if (user.ElementAt(0).UserRolesAsString.Contains(actionName))
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
