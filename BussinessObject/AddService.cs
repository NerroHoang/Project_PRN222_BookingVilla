using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class AddService
{
    public int IdBookingOnline { get; set; }

    public int IdService { get; set; }

    public int? Quantity { get; set; }

    public virtual BookingOnline IdBookingOnlineNavigation { get; set; } = null!;

    public virtual Service IdServiceNavigation { get; set; } = null!;
}
