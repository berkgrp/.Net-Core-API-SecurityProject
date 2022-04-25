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
            // to make it work out of that filter, they have to send the userID in the header of the request.
            // when we get to userID of user, we are calling that HasValidRole function in this class.
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
            // this function is controlling that user has that role or not by using its userID.
            // that query is calling the user of that userID. we are using select query because we just want
            // that user's UserRolesAsString column.
            var user = _unitOfWorkUser.RepositoryUser.IsUserHasThatRole(userId);
            if (user.Count != 0) // because of .ToList(); action, we know that it can't be null.
            {
                if (user.ElementAt(0).UserRolesAsString.Contains(actionName))
                {
                    return true;
                }
                else
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
