using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyACTS.Models;
using MyACTS.Controllers.Attributes;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;

namespace MyACTS.Controllers;

[Route("Admin/Users")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authService;

    public UsersController(ApplicationDbContext context, IAuthenticationService authService)
    {
        _context = context;
        _authService = authService;
    }

    [Authenticated]
    [Route("")]
    public IActionResult List()
    {
        return View("/Views/AdminPanel/Users/List.cshtml");
    }
}

