using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICustomerRepositories
    {
        void AddCustomer(Customer customer);

        Customer GetUserByEmail(string email);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        List<Customer> GetAllCustomers();
        bool IsEmailExist(string email);
    }
}
