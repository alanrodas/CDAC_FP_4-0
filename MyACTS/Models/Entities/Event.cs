using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyACTS.Models.Entities;

public partial class Event
{
    [Key]
    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? ActionText { get; set; }

    public string? ActionUrl { get; set; }

    public int Source { get; set; }

    public ETarget Target { get; set; }

    public int? TargetId { get; set; }

    public virtual ICollection<EventStatus> EventStatuses { get; } = new List<EventStatus>();

    public virtual User User { get; set; } = null!;
}
