using System;
using System.Collections.Generic;

namespace BussinessObject;

public partial class BookingOnline
{
    public int IdBookingOnline { get; set; }

    public DateTime CheckinDate { get; set; }

    public DateTime CheckoutDate { get; set; }

    public int? AmountOfPeople { get; set; }

    public double PriceBooking { get; set; }

    public int? IdCustomer { get; set; }

    public int IdVilla { get; set; }

    public virtual ICollection<AddService> AddServices { get; set; } = new List<AddService>();

    public virtual ICollection<CancelBooking> CancelBookings { get; set; } = new List<CancelBooking>();

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Villa? IdVillaNavigation { get; set; }

    public virtual Transaction? Transaction { get; set; }
}
