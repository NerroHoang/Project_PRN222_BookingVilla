using BussinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepositories : IAccountRepositories
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepositories(BookingVillaPrnContext context)
        {
            _accountDAO = new AccountDAO(context);
        }

        public int AddAccount(Account acc) => _accountDAO.AddAccount(acc);

        public Account CheckAccount(string username, string password) => _accountDAO.CheckAccount(username, password);
        public Customer GetCustomer(int id) => _accountDAO.GetCustomer(id);

        public Employee GetEmployee(int id) => _accountDAO.GetEmployee(id);

        public Account GetUserByUsername(string username) => _accountDAO.GetUserByUsername(username);

        public Customer GetUserByEmail(string email) => _accountDAO.GetUserByEmail(email);

        public Employee GetEmployeeByEmail(string email) => _accountDAO.GetEmployeeByEmail(email);

        public Account GetAccountByID(int id) => _accountDAO.GetAccountByID(id);

        public void UpdatePassword(int userId, string password) => _accountDAO.UpdatePassword(userId, password);

        public void UpdateInforCustomer(Customer customer) => _accountDAO.UpdateInforCustomer(customer);

        public void UpdateInforEmployee(Employee employee) => _accountDAO.UpdateInforEmployee(employee);

        public List<Account> GetAllAccounts() => _accountDAO.GetAllAccounts();

        public int GetNumberTotalAccount() => _accountDAO.GetNumberTotalAccount();

        public int GetNumberAccount() => _accountDAO.GetNumberAccount();

        public List<Account> GetAllAccountPagination(int index) => _accountDAO.GetAllAccountPagination(index);

        public bool UpdateRole(int id, string status) => _accountDAO.UpdateRole(id, status);

        public bool BanAccount(int id, bool status) => _accountDAO.BanAccount(id, status);

        public List<Account> GetAccountByName(string name) => _accountDAO.GetAccountByName(name);

        public List<Employee> SearchEmployee(string name) => _accountDAO.SearchEmployee(name);
    }
}
