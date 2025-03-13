using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class Question
{
    public int Id { get; set; }

    public int MeetingId { get; set; }

    public string QuestionText { get; set; } = null!;

    public int Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Deleted { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
