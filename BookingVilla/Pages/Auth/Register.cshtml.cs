using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessObject;
using Repository;

namespace BookingVilla.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepositories _accountRepo;
        private readonly ICustomerRepositories _customerRepo;

        public RegisterModel(IAccountRepositories accountRepo, ICustomerRepositories customerRepo)
        {
            _accountRepo = accountRepo;
            _customerRepo = customerRepo;
        }

        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string ConfirmPassword { get; set; }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Password != ConfirmPassword)
            {
                TempData["Error"] = "Password and Confirm Password do not match!";
                return Page();
            }

            // Ki?m tra n?u t�n ng�?i d�ng �? t?n t?i
            if (_accountRepo.GetUserByUsername(Username) != null)
            {
                TempData["Error"] = "Username already exists!";
                return Page();
            }

            // Ki?m tra n?u email �? t?n t?i
            if (_customerRepo.GetUserByEmail(Email) != null)
            {
                TempData["Error"] = "Email already exists!";
                return Page();
            }

            // T?o t�i kho?n m?i
            var newAccount = new Account
            {
                UserName = Username,
                PassWord = Password, // Kh�ng hash m?t kh?u (n?u c?n, h?y hash m?t kh?u tr�?c khi l�u)
                Role = "us",
                Status = true
            };
            _accountRepo.AddAccount(newAccount);

            // L?y ID c?a t�i kho?n m?i t?o
            var createdAccount = _accountRepo.GetUserByUsername(Username);
            if (createdAccount == null)
            {
                TempData["Error"] = "Failed to create an account!";
                return Page();
            }

            // T?o kh�ch h�ng li�n k?t v?i t�i kho?n v?a t?o
            var newCustomer = new Customer
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                IdCustomerNavigation = createdAccount // Li�n k?t v?i t�i kho?n �? t?o
            };
            _customerRepo.AddCustomer(newCustomer);

            return RedirectToPage("/Index");
        }
    }
}
