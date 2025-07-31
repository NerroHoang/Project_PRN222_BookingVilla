using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TransactionDAO
    {
        private readonly BookingVillaPrnContext _context;

        public TransactionDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int idBooking)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.IdTransactions == idBooking);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
        }
        public MonthRevenue GetRevenue(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year + 1, 1, 1);

            var monthlyRevenue = _context.Transactions
                .Where(t => t.Date >= startDate && t.Date < endDate)
                .GroupBy(t => new { Year = t.Date.Year, Month = t.Date.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    TotalPrice = g.Sum(t => t.Price)
                })
                .ToDictionary(x => x.Month, x => x.TotalPrice);

            return new MonthRevenue
            {
                JanRevenue = monthlyRevenue.GetValueOrDefault(1, 0),
                FebRevenue = monthlyRevenue.GetValueOrDefault(2, 0),
                MarRevenue = monthlyRevenue.GetValueOrDefault(3, 0),
                AprRevenue = monthlyRevenue.GetValueOrDefault(4, 0),
                MayRevenue = monthlyRevenue.GetValueOrDefault(5, 0),
                JuneRevenue = monthlyRevenue.GetValueOrDefault(6, 0),
                JulyRevenue = monthlyRevenue.GetValueOrDefault(7, 0),
                AugRevenue = monthlyRevenue.GetValueOrDefault(8, 0),
                SepRevenue = monthlyRevenue.GetValueOrDefault(9, 0),
                OctRevenue = monthlyRevenue.GetValueOrDefault(10, 0),
                NovRevenue = monthlyRevenue.GetValueOrDefault(11, 0),
                DecRevenue = monthlyRevenue.GetValueOrDefault(12, 0),
            };
        }
        public List<int> GetYears()
        {
            return _context.Transactions
                .Select(t => t.Date.Year) // Lấy năm từ cột Date
                .Distinct() // Loại bỏ trùng lặp
                .OrderBy(y => y) // Sắp xếp theo thứ tự tăng dần
                .ToList(); // Chuyển thành danh sách
        }

    }
}
