using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess;
using BussinessObject;
using Repository;
using Newtonsoft.Json;

namespace BookingVilla.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepositories _accountRepo;

        public LoginModel(IAccountRepositories accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _accountRepo.CheckAccount(Username, Password);
            if (user == null)
            {
                TempData["Error"] = "Username or password invalid!";
                return Page();
            }

            if (!user.Status)
            {
                TempData["Error"] = "This account was locked";
                return Page();
            }

            HttpContext.Session.SetString("UserId", user.IdAccount.ToString());
            HttpContext.Session.SetString("Role", user.Role);

            if (user.Role == "us" || user.Role == "ad")
            {
                var customerInfo = _accountRepo.GetCustomer(user.IdAccount);
                var customerDto = new
                {
                    customerInfo.IdCustomer,
                    customerInfo.Name,
                    customerInfo.Address,
                    customerInfo.Email,
                    customerInfo.Phone,
                    customerInfo.Avatar,
                    customerInfo.IdCustomerNavigation.Role // Lấy Role từ Account
                };
                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(customerDto));

            }
            else
            {
                var employeeInfo = _accountRepo.GetEmployee(user.IdAccount);
                var employeeDto = new
                {
                    employeeInfo.IdEmployee,
                    employeeInfo.Name,
                    employeeInfo.Address,
                    employeeInfo.Email,
                    employeeInfo.Phone,
                    employeeInfo.Salary,
                    employeeInfo.Avatar,
                    employeeInfo.IdEmployeeNavigation.Role // Lấy Role từ Account
                };
                HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(employeeDto));
            }

            return user.Role == "ad" ? RedirectToPage("/ManageVillaPages/ManageVilla") : RedirectToPage("/Index");
        }
    }
}

