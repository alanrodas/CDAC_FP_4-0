using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyACTS.Data;
using MyACTS.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyACTS.Controllers.Attributes;

public class WithPermissionAttribute : TypeFilterAttribute {
    public WithPermissionAttribute(EAction action, EType type, ETarget target) : base(typeof(WithPermissionFilter)) {
        Arguments = new object[] { action, type, target };
    }
}

public class WithPermissionFilter : IAuthorizationFilter
{
    private readonly ApplicationDbContext context;

    private readonly SignInManager<User> signInManager;

    private readonly UserManager<User> userManager;

    private readonly EAction action;

    private readonly EType type;

    private readonly ETarget target;

    private readonly int? targetId;

    public WithPermissionFilter(ApplicationDbContext context, SignInManager<User> signInManager,
            UserManager<User> userManager, EAction action, EType type, ETarget target) {
        this.context = context;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.action = action;
        this.type = type;
        this.target = target;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine(this);
        if (! context.HttpContext.User.Identity.IsAuthenticated) {
            context.Result = new ForbidResult();
        }
    }

    public override string ToString() {
        return $"WithPermissions(action: {this.action}, type: {this.type}, target: {this.target})";
    }
}