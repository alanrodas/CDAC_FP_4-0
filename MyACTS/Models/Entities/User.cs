using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace MyACTS.Models.Entities;

public partial class User {

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string? UserName { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string? FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateOnly? DateOfBirth { get; set; }

    public string? PasswordHash { get; set; }

    // Relationships

    public virtual IList<Event> Events { get; } = new List<Event>();

    public virtual IList<Message> Messages { get; } = new List<Message>();

    public virtual IList<Role> Roles { get; } = new List<Role>();

    public bool HasRole(string roleName) {
        return Roles != null && Roles.FirstOrDefault(r => r.Name == roleName) != null;
    }
}
