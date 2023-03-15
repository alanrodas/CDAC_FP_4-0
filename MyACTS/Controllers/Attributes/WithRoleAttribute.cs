using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyACTS.Controllers.Attributes;

public class WithRoleAttribute : TypeFilterAttribute {
    public WithRoleAttribute(string role) : base(typeof(WithRoleFilter)) {
        Arguments = new object[] { role };
    }
}

public class WithRoleFilter : AuthenticatedFilter
{
    private readonly string _roleName;

    public WithRoleFilter(ApplicationDbContext context, IAuthenticationService authService, IAppConfig appConfig, string roleName) : base(context, authService, appConfig) {
        _roleName = roleName;
    }

    public override void OnAuthorization(AuthorizationFilterContext context)
    {
        base.OnAuthorization(context);
        if ( _authService.IsLogged && !_authService.LoggedUser.HasRole(_roleName) ) {
            context.Result = new RedirectResult(_appConfig.LoginPath);
        }
    }

    public override string ToString() {
        return $"WithRole(role: {this._roleName})";
    }
}
