using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingVilla.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa session
            HttpContext.Session.Clear();

            // Xóa cookie đăng nhập (nếu có)
            Response.Cookies.Delete("cuser");
            Response.Cookies.Delete("cpass");
            Response.Cookies.Delete("crem");

            // Chuyển hướng về trang đăng nhập
            return RedirectToPage("/Index");
        }
    }
}
