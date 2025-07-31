using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Text.Json;

namespace BookingVilla.Pages.ManageBookingPages
{
    public class CanceledBookingModel : PageModel
    {
        private readonly IBookingHistoryRepositories _bookingHistoryRepositories;

        public List<BookingHistory> BookingHistoryList { get; set; }

        public CanceledBookingModel(IBookingHistoryRepositories bookingHistoryRepositories)
        {
            _bookingHistoryRepositories = bookingHistoryRepositories;
        }
        public IActionResult OnGet()
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            using JsonDocument doc = JsonDocument.Parse(userInfoJson);
            int customerId = doc.RootElement.GetProperty("IdCustomer").GetInt32();
            BookingHistoryList = _bookingHistoryRepositories.GetAllBookingHistoryStatus(customerId);
            return Page();
        }
    }
}
