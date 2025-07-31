using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageAccountPages
{
    public class BanAccountModel : PageModel
    {
        private readonly IAccountRepositories _account;
        public BanAccountModel(IAccountRepositories accountRepositories)
        {
            _account = accountRepositories;
        }
        public IActionResult OnGet(int id, string status)
        {
            bool statusban = status.ToLower() == "ban" ? false : true;
            bool result = _account.BanAccount(id, statusban);
            TempData["SuccessMessage1"] = result ? "ok" : "fail";
            return RedirectToPage("/ManageAccountPages/ManageAccount");
        }
    }
}
