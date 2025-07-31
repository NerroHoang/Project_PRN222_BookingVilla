using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDAO
    {
        private readonly BookingVillaPrnContext _context;

        public EmployeeDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả nhân viên
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.OrderBy(e => e.IdEmployee).ToList();
        }

        // Kiểm tra trạng thái của nhân viên (Dựa trên bảng Account)
        public bool CheckStatusEmployee(int id)
        {
            return _context.Accounts
                .Where(a => a.IdAccount == id)
                .Select(a => a.Status)
                .FirstOrDefault(); // Trả về giá trị bool, mặc định false nếu không tìm thấy
        }

        // Lấy nhân viên theo ID
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.IdEmployee == id);
        }

        // Tìm kiếm nhân viên theo tên, địa chỉ, email hoặc số điện thoại
        public List<Employee> GetEmployeesByName(string name)
        {
            name = name.ToLower(); // Chuyển đổi tên tìm kiếm về chữ thường

            return _context.Employees
                .Where(e => e.Name.ToLower().Contains(name) ||
                            e.Address.ToLower().Contains(name) ||
                            e.Email.ToLower().Contains(name) ||
                            e.Phone.ToLower().Contains(name))
                .ToList();
        }

        // Cập nhật lương của nhân viên
        public void UpdateSalary(int id, double salary)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.IdEmployee == id);
            if (employee != null)
            {
                employee.Salary = salary;
                _context.SaveChanges();
            }
        }
    }
}
