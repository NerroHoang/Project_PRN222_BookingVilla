using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private readonly BookingVillaPrnContext _context;

        public CustomerDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        // Thêm một khách hàng mới vào cơ sở dữ liệu
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer); // Thêm khách hàng vào DB
            _context.SaveChanges(); // Lưu thay đổi
        }

        // Tìm khách hàng theo ID


        // Tìm khách hàng theo email
        public Customer GetCustomerByEmail(string email)
        {
            return _context.Customers
                           .FirstOrDefault(c => c.Email == email); // Tìm khách hàng theo email
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers
                                            .FirstOrDefault(c => c.IdCustomer == customer.IdCustomer);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Avatar = customer.Avatar;

                _context.SaveChanges(); // Lưu thay đổi
            }
        }

        // Xóa khách hàng theo ID
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers
                                   .FirstOrDefault(c => c.IdCustomer == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer); // Xóa khách hàng
                _context.SaveChanges(); // Lưu thay đổi
            }
        }

        // Lấy danh sách tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList(); // Lấy tất cả khách hàng
        }

        // Kiểm tra xem email đã tồn tại chưa
        public bool IsEmailExist(string email)
        {
            return _context.Customers.Any(c => c.Email == email); // Kiểm tra email đã tồn tại chưa
        }
    }
}
