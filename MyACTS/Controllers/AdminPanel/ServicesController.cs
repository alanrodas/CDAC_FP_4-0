using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyACTS.Models;
using MyACTS.Controllers.Attributes;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MyACTS.Controllers;

[Route("Services")]
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServicesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authenticated]
    [Route("")]
    public IActionResult Index() {
        var data = _context.Services.Select(e => e).ToList();
        return View("/Views/AdminPanel/Services/Index.cshtml", data);
    }

    [Authenticated]
    [Route("Details/{id}")]
    public IActionResult Details(int id) {
        var data = _context.Services.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Services");
        }
        return View("/Views/AdminPanel/Services/Details.cshtml", data);
    }

    [Authenticated]
    [Route("Create")]
    public IActionResult Create() {
        return View("/Views/AdminPanel/Services/Create.cshtml", new Service());
    }

    [Authenticated]
    [HttpPost]
    [Route("Create")]
    public IActionResult DoCreate(int id, Service element) {
        try {
            _context.Services.Add(element);
            _context.SaveChanges();
            return Redirect("/Services");
        } catch ( Exception e ) {
            return View("/Views/AdminPanel/Services/Create.cshtml", element);
        }
    }

    [Authenticated]
    [Route("Edit/{id}")]
    public IActionResult Edit(int id) {
        var data = _context.Services.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Services");
        }
        return View("/Views/AdminPanel/Services/Edit.cshtml", data);
    }

    [Authenticated]
    [HttpPost]
    [Route("Edit/{id}")]
    public IActionResult DoEdit(int id, Service element) {
        try {
            element.Id = id;
            _context.Services.Update(element);
            _context.SaveChanges();
            return Redirect("/Services");
        } catch ( Exception e ) {
            return View("/Views/AdminPanel/Services/Create.cshtml", element);
        }
    }

    [Authenticated]
    [Route("Delete/{id}")]
    public IActionResult Delete(int id) {
        var data = _context.Services.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Services");
        }
        return View("/Views/AdminPanel/Services/Delete.cshtml", data);
    }

    [Authenticated]
    [HttpPost]
    [Route("Delete/{id}")]
    public IActionResult DoDelete(int id) {
        var data = _context.Services.Where(e => e.Id == id).FirstOrDefault();
        _context.Services.Remove(data);
        _context.SaveChanges();
        return Redirect("/Services");
    }
}

