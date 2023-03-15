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
using System.Xml.Linq;

namespace MyACTS.Controllers;

[Route("Messages")]
public class MessagesController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IAuthenticationService _authService;

    public MessagesController(ApplicationDbContext context, IAuthenticationService authservice) {
        _context = context;
        _authService = authservice;
    }

    [Authenticated]
    [Route("")]
    public IActionResult Index() {
        var data = _context.Messages.Select(e => e).ToList();
        return View("/Views/AdminPanel/Messages/Index.cshtml", data);
    }

    [Authenticated]
    [Route("Details/{id}")]
    public IActionResult Details(int id) {
        var data = _context.Messages.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Messages");
        }
        return View("/Views/AdminPanel/Messages/Details.cshtml", data);
    }

    [Authenticated]
    [Route("Create")]
    public IActionResult Create() {
        return View("/Views/AdminPanel/Messages/Create.cshtml", new Message());
    }

    [Authenticated]
    [HttpPost]
    [Route("Create")]
    public IActionResult DoCreate(int id, Message element) {
        try {
            element.User = _authService.LoggedUser;
            _context.Messages.Add(element);
            _context.SaveChanges();
            return Redirect("/Messages");
        } catch ( Exception e ) {
            return View("/Views/AdminPanel/Messages/Create.cshtml", element);
        }
    }

    [Authenticated]
    [Route("Edit/{id}")]
    public IActionResult Edit(int id) {
        var data = _context.Messages.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Messages");
        }
        return View("/Views/AdminPanel/Messages/Edit.cshtml", data);
    }

    [Authenticated]
    [HttpPost]
    [Route("Edit/{id}")]
    public IActionResult DoEdit(int id, Message element) {
        try {
            element.Id = id;
            _context.Messages.Update(element);
            _context.SaveChanges();
            return Redirect("/Messages");
        } catch ( Exception e ) {
            return View("/Views/AdminPanel/Messages/Create.cshtml", element);
        }
    }

    [Authenticated]
    [Route("Delete/{id}")]
    public IActionResult Delete(int id) {
        var data = _context.Messages.Where(e => e.Id == id).FirstOrDefault();
        if ( data == null ) {
            return Redirect("/Messages");
        }
        return View("/Views/AdminPanel/Messages/Delete.cshtml", data);
    }

    [Authenticated]
    [HttpPost]
    [Route("Delete/{id}")]
    public IActionResult DoDelete(int id) {
        var data = _context.Messages.Where(e => e.Id == id).FirstOrDefault();
        _context.Messages.Remove(data);
        _context.SaveChanges();
        return Redirect("/Messages");
    }
}

