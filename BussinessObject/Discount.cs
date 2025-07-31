using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Discount
{
    public int IdDiscount { get; set; }

    public string? Code { get; set; }

    public decimal? Percents { get; set; }

    public bool? Status { get; set; }

    public int? IdAccount { get; set; }

    public virtual Account? IdAccountNavigation { get; set; }
}
