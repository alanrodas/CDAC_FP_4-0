using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyACTS.Models.Entities;

public partial class Service
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(255)]
    [Url]
    public string Url { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    [Display(Name="Image")]
    [Url]
    public string ImageUrl { get; set; } = null!;

    // TODO Save image as blob in 64x64 format
    // requires scaling image also.
    public byte[]? Icon { get; set; }
}
