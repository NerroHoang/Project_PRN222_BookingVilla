using BussinessObject;

public class BookingDAO
{
    private readonly BookingVillaPrnContext _context;

    public BookingDAO(BookingVillaPrnContext context)
    {
        _context = context;
    }

    public int AddBooking(BookingOnline booking)
    {
        _context.BookingOnlines.Add(booking);
        _context.SaveChanges();
        return booking.IdBookingOnline; // EF sẽ tự cập nhật Id sau khi SaveChanges()
    }

    public void AddServiceBooking(int bookingId, AddService addService)
    {
        addService.IdBookingOnline = bookingId;
        _context.AddServices.Add(addService);
        _context.SaveChanges();
    }
}
