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
using Microsoft.EntityFrameworkCore;

namespace MyACTS.Services;

public class NonExistingUserException : Exception { }
public class AuthenticationFailedException : Exception { }
public class NoLoggedUserException : Exception { }
public class AlreadyLoggedException : Exception { }

public interface IAuthenticationService {
    User Login(string username, string password);
    void Logout();
    bool IsLogged { get; }
    User LoggedUser { get; }
}

public class AuthenticationService : IAuthenticationService
{
    private HttpContext _httpContext;
    private readonly ApplicationDbContext _context;
    private User? _currentUser;

    public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) {
        _context = context;
        _httpContext = httpContextAccessor.HttpContext;
        _currentUser = null;
    }

    public User Login(string username, string password) {
        if (IsLogged) {
            throw new AlreadyLoggedException();
        }
        var User = _context.Users.SingleOrDefault(u => u.UserName == username);
        if ( User == null ) {
            throw new NonExistingUserException();
        }

        if ( Crypto.VerifyHashedPassword(User.PasswordHash, password) ) {
            _currentUser = User;
            var claimsIdentity = new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim("FullName", User.FullName),
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            _httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();

            return User;
        } else {
            _context.SaveChanges();
            throw new AuthenticationFailedException();
        }
    }

    public void Logout() {
        if ( !IsLogged ) {
            throw new NoLoggedUserException();
        }
        _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        _currentUser = null;
        _context.SaveChanges();
    }

    public bool IsLogged {
        get {
            var claim = _httpContext.User;
            return claim != null && claim.Identity != null && claim.Identity.IsAuthenticated;
        }
    }

    public User LoggedUser {
        get {
            if ( !IsLogged ) {
                throw new NoLoggedUserException();
            }
            if ( _currentUser != null ) {
                return _currentUser;
            }
            var claim = _httpContext.User;
            _currentUser = _context.Users
                .Include(u => u.Roles)
                .SingleOrDefault(u => u.UserName == claim.Identity.Name);
            return _currentUser;
        }
    }
}

