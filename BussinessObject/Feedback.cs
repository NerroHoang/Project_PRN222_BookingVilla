using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Feedback
{
    public int IdFeedback { get; set; }

    public string? ContentFeedback { get; set; }

    public DateOnly? Date { get; set; }

    public int? IdVilla { get; set; }

    public int? IdAccount { get; set; }

    public int? Point { get; set; }

    public virtual Account? IdAccountNavigation { get; set; }

    public virtual Villa? IdVillaNavigation { get; set; }
}
