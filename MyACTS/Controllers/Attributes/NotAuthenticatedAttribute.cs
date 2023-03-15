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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyACTS.Controllers.Attributes;

public class NotAuthenticatedAttribute : TypeFilterAttribute {
    public NotAuthenticatedAttribute() : base(typeof(NotAuthenticatedFilter)) {
    }
}

public class NotAuthenticatedFilter : IAuthorizationFilter
{
    protected readonly ApplicationDbContext _context;

    protected readonly IAuthenticationService _authService;

    protected readonly IAppConfig _appConfig;

    public NotAuthenticatedFilter(ApplicationDbContext context, IAuthenticationService authService, IAppConfig appConfig) {
        _context = context;
        _authService = authService;
        _appConfig = appConfig;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if ( _authService.IsLogged ) {
            context.Result = new RedirectResult(_appConfig.ReturnUrlParameter);
        }
    }

    public override string ToString() {
        return $"NotAuthenticated()";
    }
}