using System;
using System.Collections.Generic;

namespace MyACTS.Models.Entities;

public partial class MessageStatus
{
    public int MessageId { get; set; }

    public int UserId { get; set; }

    public EStatus? Status { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? CurrentSessionToken { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
