using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DiscountRepository
    {
        private readonly BookingVillaPrnContext _context;

        public DiscountRepository(BookingVillaPrnContext context)
        {
            _context = context;
        }

        public void AddDiscountForNewUser(int id, string discountCode)
        {
            var discount = new Discount
            {
                Code = discountCode,
                Percents = 5,
                Status = true,
                IdAccount = id
            };

            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public Discount? GetDiscount(int id, string code)
        {
            return _context.Discounts
                .FirstOrDefault(d => d.IdAccount == id && d.Code == code && d.Status == true);
        }

        public void UpdateStatusDiscount(int id, string code)
        {
            var discount = _context.Discounts
                .FirstOrDefault(d => d.Code == code && d.IdAccount == id);

            if (discount != null)
            {
                discount.Status = false;
                _context.SaveChanges();
            }
        }
    }

}
