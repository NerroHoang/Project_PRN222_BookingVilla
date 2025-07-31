using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageAccountPages
{
    public class UpdateRoleAccountModel : PageModel
    {
        private readonly IAccountRepositories _account;
        public UpdateRoleAccountModel(IAccountRepositories accountRepositories)
        {
            _account = accountRepositories;
        }
        public IActionResult OnGet(int id, string role)
        {
            bool result = _account.UpdateRole(id, role);
            TempData["SuccessMessage"] = result ? "ok" : "fail";
            return RedirectToPage("/ManageAccountPages/ManageAccount");
        }
    }
}
