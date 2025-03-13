using System;
using System.Collections.Generic;

namespace EventAPI.Models;

public partial class Vote
{
    public int Id { get; set; }

    public int MeetingId { get; set; }

    public int QuestionId { get; set; }

    public int QuestionAnswerId { get; set; }

    public string QuestionAnswerText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool Deleted { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionAnswer QuestionAnswer { get; set; } = null!;
}
