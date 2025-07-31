using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingRepositories : IBookingRepositories
    {
        private readonly BookingDAO _bookingDAO;

        public BookingRepositories(BookingVillaPrnContext context)
        {
            _bookingDAO = new BookingDAO(context);
        }
        public int AddBooking(BookingOnline booking) => _bookingDAO.AddBooking(booking);

        public void AddServiceBooking(int bookingId, AddService addService) => _bookingDAO.AddServiceBooking(bookingId, addService);
    }
}
