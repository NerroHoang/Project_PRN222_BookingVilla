using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<BookingOnline> BookingOnlines { get; set; } = new List<BookingOnline>();

    public virtual ICollection<CancelBooking> CancelBookings { get; set; } = new List<CancelBooking>();

    public virtual Account IdCustomerNavigation { get; set; } = null!;
}
