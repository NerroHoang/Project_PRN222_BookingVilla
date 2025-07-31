using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepositories
    {
        public int AddAccount(Account acc);
        public Account CheckAccount(string username, string password);
        public Customer GetCustomer(int id);
        public Employee GetEmployee(int id);
        public Account GetUserByUsername(string username);
        public Customer GetUserByEmail(string email);
        public Employee GetEmployeeByEmail(string email);
        public Account GetAccountByID(int id);
        public void UpdatePassword(int userId, string password);
        public void UpdateInforCustomer(Customer customer);
        public void UpdateInforEmployee(Employee employee);
        public List<Account> GetAllAccounts();
        public int GetNumberTotalAccount();
        public int GetNumberAccount();
        public List<Account> GetAllAccountPagination(int index);
        public bool UpdateRole(int id, string status);
        public bool BanAccount(int id, bool status);
        public List<Account> GetAccountByName(string name);
        public List<Employee> SearchEmployee(string name);
    }
}
