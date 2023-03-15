using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyACTS.Models;
using MyACTS.Controllers.Attributes;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;

namespace MyACTS.Controllers;

[Route("Admin")]
public class AdminPanelController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authService;

    public AdminPanelController(ApplicationDbContext context, IAuthenticationService authService)
    {
        _context = context;
        _authService = authService;
    }

    [Authenticated]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }
}

