using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyACTS.Models.Entities;

public partial class Permission
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public EAction Action { get; set; }

    public EType Type { get; set; }

    public ETarget Target { get; set; }

    public int? IdTarget { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
