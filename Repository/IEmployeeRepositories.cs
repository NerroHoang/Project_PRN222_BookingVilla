using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IEmployeeRepositories
    {
        public List<Employee> GetAllEmployees();
        public bool CheckStatusEmployee(int id);
        public Employee GetEmployeeById(int id);
        public List<Employee> GetEmployeesByName(string search);
        public void UpdateSalary(int id, double salary);
    }
}
