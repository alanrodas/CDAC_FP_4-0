using System;
using System.Collections.Generic;

namespace MyACTS.Models.Entities;

public partial class EventStatus
{
    public int EventId { get; set; }

    public int UserId { get; set; }

    public EStatus? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? CurrentSessionToken { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
