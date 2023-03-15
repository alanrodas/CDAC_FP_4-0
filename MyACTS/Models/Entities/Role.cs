using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyACTS.Models.Entities;

public partial class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string? Name { get; set; } = null!;

    [MaxLength(150)]
    public string? Description { get; set; }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
