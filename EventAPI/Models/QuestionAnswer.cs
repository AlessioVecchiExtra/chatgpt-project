using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class QuestionAnswer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string? AnswerText { get; set; }

    public int Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Deleted { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
