using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class Meeting
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
