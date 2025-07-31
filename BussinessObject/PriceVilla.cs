using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class PriceVilla
{
    public int IdPriceVilla { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public double PriceDay { get; set; }

    public int? IdVilla { get; set; }

    public virtual Villa? IdVillaNavigation { get; set; }
}
