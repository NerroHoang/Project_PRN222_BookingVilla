using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepositories : IEmployeeRepositories
    {
        private readonly EmployeeDAO _employeeDAO;

        public EmployeeRepositories(BookingVillaPrnContext context)
        {
            _employeeDAO = new EmployeeDAO(context);
        }
        public bool CheckStatusEmployee(int id) => _employeeDAO.CheckStatusEmployee(id);

        public List<Employee> GetAllEmployees() => _employeeDAO.GetAllEmployees();

        public Employee GetEmployeeById(int id) => _employeeDAO.GetEmployeeById(id);

        public List<Employee> GetEmployeesByName(string search) => _employeeDAO.GetEmployeesByName(search);

        public void UpdateSalary(int id, double salary) => _employeeDAO.UpdateSalary(id, salary);
    }
}
