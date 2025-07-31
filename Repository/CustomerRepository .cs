using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CustomerRepository : ICustomerRepositories
    {
        private readonly CustomerDAO _customerDAO;

        public CustomerRepository(BookingVillaPrnContext context)
        {
            _customerDAO = new CustomerDAO(context);
        }

        // Thêm một khách hàng mới vào cơ sở dữ liệu
        public void AddCustomer(Customer customer)
        {
            _customerDAO.AddCustomer(customer); // Gọi phương thức AddCustomer từ CustomerDAO
        }

        // Tìm khách hàng theo ID


        // Tìm khách hàng theo email
        public Customer GetUserByEmail(string email)
        {
            return _customerDAO.GetCustomerByEmail(email); // Gọi phương thức GetCustomerByEmail từ CustomerDAO
        }

        // Cập nhật thông tin khách hàng
        public void UpdateCustomer(Customer customer)
        {
            _customerDAO.UpdateCustomer(customer); // Gọi phương thức UpdateCustomer từ CustomerDAO
        }

        // Xóa khách hàng theo ID
        public void DeleteCustomer(int id)
        {
            _customerDAO.DeleteCustomer(id); // Gọi phương thức DeleteCustomer từ CustomerDAO
        }

        // Lấy danh sách tất cả khách hàng
        public List<Customer> GetAllCustomers()
        {
            return _customerDAO.GetAllCustomers(); // Gọi phương thức GetAllCustomers từ CustomerDAO
        }

        // Kiểm tra email đã tồn tại hay chưa
        public bool IsEmailExist(string email)
        {
            return _customerDAO.IsEmailExist(email); // Gọi phương thức IsEmailExist từ CustomerDAO
        }
    }
}
