using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICancelBookingRepositories
    {
        public bool AddCancelBooking(CancelBooking cancelBooking);
        public List<ManageCancelBooking> GetAllCancelBookingsPagination(string status, int pageIndex, int pageSize);
        public List<ManageCancelBooking> GetAllCancelBookingsPaginationAll(int pageIndex, int pageSize);
        public int GetTotalCancelBookingCountByStatus(string status);
        public int GetCancelBookingPageCountByStatus(string status, int pageSize);
        public int GetTotalCancelBookingCount();
        public int GetCancelBookingPageCount(int pageSize);
        public void UpdateStatusCancelBooking(int id, string status);
        public List<ManageCancelBooking> SearchCancelBookings(string keyword);
    }
}
