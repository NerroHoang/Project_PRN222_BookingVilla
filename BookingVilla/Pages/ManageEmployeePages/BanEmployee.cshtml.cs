using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageEmployeePages
{
    public class BanEmployeeModel : PageModel
    {
        private readonly IAccountRepositories _account;
        public BanEmployeeModel(IAccountRepositories accountRepositories)
        {
            _account = accountRepositories;
        }
        public IActionResult OnGet(int id, string status)
        {
            bool statusban = status.ToLower() == "ban" ? false : true;
            bool result = _account.BanAccount(id, statusban);
            TempData["SuccessMessage"] = result ? "ok" : "fail";
            return RedirectToPage("/ManageEmployeePages/ManageEmployee");
        }
    }
}
