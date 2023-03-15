using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CryptoHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MyACTS.Data;
using MyACTS.Models.Entities;
using System.Threading.Tasks;

namespace MyACTS.Services;

public interface IAuthorizationService {
    bool IsAuthorized(User user, EAction action, EType type, ETarget target);
    bool IsAuthorized(User user, EAction action, EType type, ETarget target, long targetId);
}

public class AuthorizationService : IAuthorizationService
{
    private HttpContext _httpContext;
    private readonly ApplicationDbContext _context;
    private User? _currentUser;

    public AuthorizationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _httpContext = httpContextAccessor.HttpContext;
    }

    public bool IsAuthorized(User user, EAction action, EType type, ETarget target) {
        // _context.Permissions.Select
        return false;
    }

    public bool IsAuthorized(User user, EAction action, EType type, ETarget target, long targetId) {
        return false;
    }
}

