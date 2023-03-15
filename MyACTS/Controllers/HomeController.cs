using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyACTS.Models;
using MyACTS.Controllers.Attributes;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;
using System.Linq;

namespace MyACTS.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authService;
    private readonly IAppConfig _appConfig;

    public HomeController(ApplicationDbContext context, IAuthenticationService authService, IAppConfig appConfig)
    {
        _context = context;
        _authService = authService;
        _appConfig = appConfig;
    }

    [Authenticated]
    public IActionResult Index()
    {

        if ( _authService.LoggedUser.HasRole("admin") ) {
            return Redirect(_appConfig.RoleRedirects["admin"]);
        }
        if ( _authService.LoggedUser.HasRole("student") ) {
            return Redirect(_appConfig.RoleRedirects["student"]);
        }
        return Redirect(_appConfig.RoleRedirects["student"]);
    }

    [Authenticated]
    [Route("/Dashboard")]
    public IActionResult Dashboard() {
        ViewBag.Messages = _context.Messages.Select(e => e).ToList();
        ViewBag.Services = _context.Services.Select(e => e).ToList();
        return View();
    }

    [Authenticated]
    [WithRole("student")]
    public IActionResult DismissMessage() {
        return View();
    }

    [Authenticated]
    [WithRole("student")]
    public IActionResult OpenService(int serviceId) {
        var service = _context.Services.Find(serviceId);
        if ( service != null ) {
            return Redirect(service.Url);
        } else {
            return View();
        }
    }
}
