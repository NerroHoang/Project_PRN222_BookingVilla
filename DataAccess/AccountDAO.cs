using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        private readonly BookingVillaPrnContext _context;

        public AccountDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        public int AddAccount(Account acc)
        {
            _context.Accounts.Add(acc);
            _context.SaveChanges();
            return acc.IdAccount;
        }
        public List<Account> GetAllAccounts()
        {
            return _context.Accounts
                .OrderByDescending(a => a.Status)
                .ToList();
        }

        public int GetNumberTotalAccount()
        {
            return _context.Accounts.Count();
        }

        public int GetNumberAccount()
        {
            int total = _context.Accounts.Count();
            int countPage = total / 5;
            if (total % 5 != 0)
            {
                countPage++;
            }
            return countPage;
        }
        public Account CheckAccount(string username, string password)
        {
            return _context.Accounts
                .FirstOrDefault(a => a.UserName == username && a.PassWord == password);
        }
        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.IdCustomer == id);
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.IdEmployee == id);
        }

        public Account GetUserByUsername(string username)
        {
            return _context.Accounts.FirstOrDefault(a => a.UserName == username);
        }

        public Customer GetUserByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == email);
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(e => e.Email == email);
        }

        public Account GetAccountByID(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.IdAccount == id);
        }

        public void UpdatePassword(int userId, string password)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.IdAccount == userId);
            if (account != null)
            {
                account.PassWord = password;
                _context.SaveChanges();
            }
        }

        public void UpdateInforCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.IdCustomer == customer.IdCustomer);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Address = customer.Address;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Avatar = customer.Avatar;
                _context.SaveChanges();
            }
        }

        public void UpdateInforEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.IdEmployee == employee.IdEmployee);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Address = employee.Address;
                existingEmployee.Email = employee.Email;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Avatar = employee.Avatar;
                _context.SaveChanges();
            }
        }
        public List<Account> GetAllAccountPagination(int index)
        {
            {
                return _context.Accounts
                    .OrderByDescending(a => a.IdAccount) // Sắp xếp theo Status DESC
                    .Skip((index - 1) * 12) // OFFSET
                    .Take(12) // FETCH FIRST 5 ROWS ONLY
                    .ToList();
            }
        }
        public bool UpdateRole(int id, string status)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.IdAccount == id);
            if (account != null)
            {
                account.Role = status;
                _context.Update(account);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool BanAccount(int id, bool status)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.IdAccount == id);
            if (account != null)
            {
                account.Status = status;
                _context.Update(account);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public List<Account> GetAccountByName(string name)
        {
            return _context.Accounts
                .Where(a => a.UserName.ToLower().Contains(name.ToLower()))
                .ToList();
        }
        public List<Employee> SearchEmployee(string name)
        {
            name = name.ToLower(); // Chuyển đổi tên tìm kiếm về chữ thường

            return _context.Employees
                .Where(e => e.Name.ToLower().Contains(name) ||
                            e.Address.ToLower().Contains(name) ||
                            e.Email.ToLower().Contains(name) ||
                            e.Phone.ToLower().Contains(name))
                .ToList();
        }

    }
}
