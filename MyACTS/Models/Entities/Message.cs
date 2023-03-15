using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyACTS.Models.Entities;

public partial class Message
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Subject { get; set; } = null!;

    [Required]
    public string Body { get; set; } = null!;

    [MaxLength(255)]
    [Display(Name="Name of the action")]
    public string? ActionText { get; set; }

    [MaxLength(255)]
    [Display(Name="Url of the action")]
    [Url]
    public string? ActionUrl { get; set; }

    public int Source { get; set; }

    public ETarget Target { get; set; }

    public int? TargetId { get; set; }

    public virtual ICollection<MessageStatus> MessageStatuses { get; } = new List<MessageStatus>();

    public virtual User User { get; set; } = null!;
}
