using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CancelBookingDAO
    {
        private readonly BookingVillaPrnContext _context;

        public CancelBookingDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        // 1. Thêm yêu cầu hủy đặt phòng
        public bool AddCancelBooking(CancelBooking cancelBooking)
        {
            try
            {
                _context.CancelBookings.Add(cancelBooking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 2. Lấy danh sách hủy đặt phòng theo trạng thái
        public List<ManageCancelBooking> GetAllCancelBookings(string status)
        {
            return _context.CancelBookings
                .Where(c => c.Status == status)
                .Select(c => new ManageCancelBooking
                {
                    Id = c.Id,
                    IdBookingOnline = c.IdBookingOnline,
                    IdCustomer = c.IdCustomer,
                    RequestDate = c.RequestDate,
                    Status = c.Status,
                    Name = c.IdCustomerNavigation.Name,
                    Phone = c.IdCustomerNavigation.Phone
                })
                .OrderByDescending(c => c.Id)
                .ToList();
        }

        // 3. Lấy danh sách hủy đặt phòng có phân trang theo trạng thái
        public List<ManageCancelBooking> GetAllCancelBookingsPagination(string status, int pageIndex, int pageSize)
        {
            return _context.CancelBookings
                .Where(c => c.Status == status)
                .Select(c => new ManageCancelBooking
                {
                    Id = c.Id,
                    IdBookingOnline = c.IdBookingOnline,
                    IdCustomer = c.IdCustomer,
                    RequestDate = c.RequestDate,
                    Status = c.Status,
                    Name = c.IdCustomerNavigation.Name,
                    Phone = c.IdCustomerNavigation.Phone
                })
                .OrderByDescending(c => c.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        // 4. Lấy danh sách hủy đặt phòng có phân trang (tất cả trạng thái)
        public List<ManageCancelBooking> GetAllCancelBookingsPaginationAll(int pageIndex, int pageSize)
        {
            return _context.CancelBookings
                .Select(c => new ManageCancelBooking
                {
                    Id = c.Id,
                    IdBookingOnline = c.IdBookingOnline,
                    IdCustomer = c.IdCustomer,
                    RequestDate = c.RequestDate,
                    Status = c.Status,
                    Name = c.IdCustomerNavigation.Name,
                    Phone = c.IdCustomerNavigation.Phone
                })
                .OrderByDescending(c => c.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        public int GetTotalCancelBookingCount()
        {
            using (var context = new BookingVillaPrnContext())
            {
                return context.CancelBookings.Count();
            }
        }

        public int GetCancelBookingPageCount(int pageSize = 5)
        {
            using (var context = new BookingVillaPrnContext())
            {
                int totalRecords = context.CancelBookings.Count();
                return (int)Math.Ceiling((double)totalRecords / pageSize);
            }
        }
        public int GetTotalCancelBookingCountByStatus(string status)
        {
            using (var context = new BookingVillaPrnContext())
            {
                return context.CancelBookings.Count(cb => cb.Status == status);
            }
        }
        public int GetCancelBookingPageCountByStatus(string status, int pageSize)
        {
            using (var context = new BookingVillaPrnContext())
            {
                int totalRecords = context.CancelBookings
                    .Where(cb => cb.Status == status)
                    .Count();

                return (int)Math.Ceiling((double)totalRecords / pageSize);
            }
        }
        public void UpdateStatusCancelBooking(int id, string status)
        {
            var cancelBooking = _context.CancelBookings.FirstOrDefault(cb => cb.Id == id);
            if (cancelBooking != null)
            {
                cancelBooking.Status = status;
                _context.SaveChanges();
            }
        }
        public List<ManageCancelBooking> SearchCancelBookings(string keyword)
        {
            return _context.CancelBookings
                .Join(_context.Customers,
                    cancelBooking => cancelBooking.IdCustomer,
                    customer => customer.IdCustomer,
                    (cancelBooking, customer) => new
                    {
                        cancelBooking.Id,
                        cancelBooking.IdBookingOnline,
                        cancelBooking.IdCustomer,
                        cancelBooking.RequestDate,
                        cancelBooking.Status,
                        customer.Name,
                        customer.Phone
                    })
                .Where(cb => cb.Name.Contains(keyword) || cb.Phone.Contains(keyword))
                .OrderByDescending(cb => cb.Id)
                .Select(cb => new ManageCancelBooking
                {
                    Id = cb.Id,
                    IdBookingOnline = cb.IdBookingOnline,
                    IdCustomer = cb.IdCustomer,
                    RequestDate = cb.RequestDate,
                    Status = cb.Status,
                    Name = cb.Name,
                    Phone = cb.Phone
                })
                .ToList();
        }
    }
}
