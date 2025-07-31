using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CancelBookingRepositories : ICancelBookingRepositories
    {
        private readonly CancelBookingDAO _cancelbookingDAO;

        public CancelBookingRepositories(BookingVillaPrnContext context)
        {
            _cancelbookingDAO = new CancelBookingDAO(context);
        }
        public bool AddCancelBooking(CancelBooking cancelBooking) => _cancelbookingDAO.AddCancelBooking(cancelBooking);

        public List<ManageCancelBooking> GetAllCancelBookings(string status) => _cancelbookingDAO.GetAllCancelBookings(status);

        public List<ManageCancelBooking> GetAllCancelBookingsPagination(string status, int pageIndex, int pageSize) => _cancelbookingDAO.GetAllCancelBookingsPagination(status, pageIndex, pageSize);

        public List<ManageCancelBooking> GetAllCancelBookingsPaginationAll(int pageIndex, int pageSize) => _cancelbookingDAO.GetAllCancelBookingsPaginationAll(pageIndex, pageSize);

        public int GetCancelBookingPageCount(int pageSize) => _cancelbookingDAO.GetCancelBookingPageCount(pageSize);

        public int GetCancelBookingPageCountByStatus(string status, int pageSize) => _cancelbookingDAO.GetCancelBookingPageCountByStatus(status, pageSize);

        public int GetTotalCancelBookingCountByStatus(string status) => _cancelbookingDAO.GetTotalCancelBookingCountByStatus(status);

        public int GetTotalCancelBookingCount() => _cancelbookingDAO.GetTotalCancelBookingCount();

        public void UpdateStatusCancelBooking(int id, string status) => _cancelbookingDAO.UpdateStatusCancelBooking(id, status);

        public List<ManageCancelBooking> SearchCancelBookings(string keyword) => _cancelbookingDAO.SearchCancelBookings(keyword);
    }
}
