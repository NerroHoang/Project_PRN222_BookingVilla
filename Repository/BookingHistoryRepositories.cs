using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingHistoryRepositories : IBookingHistoryRepositories
    {
        private readonly BookingHistoryDAO _bookinghistoryDAO;

        public BookingHistoryRepositories(BookingVillaPrnContext context)
        {
            _bookinghistoryDAO = new BookingHistoryDAO(context);
        }
        public List<BookingHistory> GetAllBookingHistoryNoStatus(int idCustomer) => _bookinghistoryDAO.GetAllBookingHistoryNoStatus(idCustomer);

        public List<BookingHistory> GetAllBookingHistoryStatus(int idCustomer) => _bookinghistoryDAO.GetAllBookingHistoryStatus(idCustomer);

        public List<BookingHistory> GetAllBookingHistoryNoStatusPagination(int idCustomer, int index) => _bookinghistoryDAO.GetAllBookingHistoryNoStatusPagination(idCustomer, index);

        public List<BookingHistory> GetAllBookingHistoryStatusPagination(int idCustomer, int index) => _bookinghistoryDAO.GetAllBookingHistoryStatusPagination(idCustomer, index);

        public int GetNumberBookingHistoryNoStatus(int idCustomer) => _bookinghistoryDAO.GetNumberBookingHistoryNoStatus(idCustomer);

        public int GetNumberBookingHistoryStatus(int idCustomer) => _bookinghistoryDAO.GetNumberBookingHistoryStatus(idCustomer);

        public int GetNumberTotalBookingHistoryNoStatus(int idCustomer) => _bookinghistoryDAO.GetNumberTotalBookingHistoryNoStatus(idCustomer);

        public int GetNumberTotalBookingHistoryStatus(int idCustomer) => _bookinghistoryDAO.GetNumberTotalBookingHistoryStatus(idCustomer);
    }
}
