﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceLayer.Models;
using ServiceLayer.RoleAttributes;
using ServiceLayer.Roles;
using System;
using System.Linq;

namespace ApiProject.Helpers
{
    public class PermissionFilter : IActionFilter
    {
        private readonly IRoleService _roleService;
        public PermissionFilter(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Rol kontrolü için kullanıcı bilgisi header'dan alınır
            Guid userGuidID = Guid.Empty;
            _=Guid.TryParse(context.HttpContext.Request.Headers["UserGuidId"].FirstOrDefault(),out userGuidID);
            if (HasRoleAttribute(context))
            {
                try
                {
                    var arguments = ((ControllerActionDescriptor)context.ActionDescriptor)
                        .MethodInfo.CustomAttributes.FirstOrDefault(fd => fd.AttributeType == typeof(RoleAttribute))
                        .ConstructorArguments;

                    int roleGroupID = (int)arguments[0].Value;
                    Int64 roleID = (Int64)arguments[1].Value;
                    RoleModel role = _roleService.GetRoleById(userGuidID, roleGroupID, roleID).Entity;
                    if (role == null || role.Id == 0)
                    {
                        context.Result = new ObjectResult(context.ModelState)
                        {
                            Value = "You are not authorized for this page, talk with the admin for the role attributes.",
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden
                        };
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public bool HasRoleAttribute(FilterContext context)
        {
            return ((ControllerActionDescriptor)context.ActionDescriptor)
                .MethodInfo.CustomAttributes.Any(filterDescriptors =>
                filterDescriptors.AttributeType == typeof(RoleAttribute));
        }
    }
}