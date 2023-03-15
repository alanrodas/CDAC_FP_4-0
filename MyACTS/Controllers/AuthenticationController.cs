using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyACTS.Data;
using CryptoHelper;
using MyACTS.Services;
using MyACTS.Models.Entities;
using MyACTS.Controllers.Attributes;

namespace MyACTS.Controllers;

[Route("Auth")]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authService;
    private readonly IAppConfig _appConfig;

    public AuthController(ApplicationDbContext context, IAuthenticationService authService, IAppConfig config)
    {
        _context = context;
        _authService = authService;
        _appConfig = config;
    }

    // GET: /Auth/Login
    [HttpGet]
    [Route("Login")]
    [NotAuthenticated]
    public IActionResult Login()
    {
        /*
        if ( _authService.IsLogged ) {
            return Redirect(_appConfig.ReturnUrlParameter);
        }
        */
        return View();
    }

    // GET: /Auth/CreteTestUser
    [HttpGet]
    [Route("CreteTestUser")]
    public IActionResult CreteTestUser() {
        _context.Users.Add(new User() {
            UserName = "johndoe",
            FullName = "John Doe",
            Email = "johndoe@company.com",
            PhoneNumber = "12345",
            DateOfBirth = new DateOnly(),
            PasswordHash = Crypto.HashPassword("123456789")
        });
        _context.SaveChanges();
        return View();
    }

    // POST: /Auth/Login
    [HttpPost]
    [Route("Login")]
    [NotAuthenticated]
    public IActionResult Login(string username, string password) {
        try {
            _authService.Login(username, password);
            return Redirect("/");
        } catch (Exception e) {
            return View(e);
        }
    }

    // POST: /Auth/ChangePassword
    [HttpPost]
    [Route("ChangePassword")]
    [Authenticated]
    public IActionResult ChangePassword(string username, string oldPassword, string newPassword, string newPasswordConfirm) {
        var User = _context.Users.GetWithCredentials(username, oldPassword);
        if ( User == null ) {
            return View(/* Invalid user */);
        }
        if ( !newPassword.IsValidAsPassword() ) {
            return View( /* Invalid password error */ );
        }
        if ( newPassword.IsValidAsPassword() && newPassword != newPasswordConfirm ) {
            return View( /* Password confirm do not match */ );
        }
        _context.Users.SetUserPassword(User, newPassword);
        _context.SaveChanges();
        return Redirect("/");
    }

    // GET: /Auth/Logout
    [HttpGet]
    [Route("Logout")]
    [Authenticated]
    public IActionResult Logout() {
        _authService.Logout();
        return Redirect("/");
    }
}

