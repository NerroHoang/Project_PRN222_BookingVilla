using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class CancelBooking
{
    public int Id { get; set; }

    public int IdBookingOnline { get; set; }

    public int IdCustomer { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public virtual BookingOnline IdBookingOnlineNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
