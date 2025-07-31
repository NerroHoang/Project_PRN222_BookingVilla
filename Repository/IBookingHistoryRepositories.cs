using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBookingHistoryRepositories
    {
        List<BookingHistory> GetAllBookingHistoryStatus(int idCustomer);
        public List<BookingHistory> GetAllBookingHistoryStatusPagination(int idCustomer, int index);
        public int GetNumberTotalBookingHistoryStatus(int idCustomer);
        public int GetNumberBookingHistoryStatus(int idCustomer);
        public List<BookingHistory> GetAllBookingHistoryNoStatusPagination(int idCustomer, int index);
        public int GetNumberTotalBookingHistoryNoStatus(int idCustomer);
        public int GetNumberBookingHistoryNoStatus(int idCustomer);
    }
}
