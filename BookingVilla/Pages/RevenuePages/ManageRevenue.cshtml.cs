using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.RevenuePages
{
    public class ManageRevenueModel : PageModel
    {
        private readonly ITransactionRepositories _transactionRepositories;
        [BindProperty(SupportsGet = true)]
        public int YearChoosing { get; set; }

        public List<int> Years { get; set; } = new List<int>();
        public MonthRevenue Revenue { get; set; } = new MonthRevenue();

        public ManageRevenueModel(ITransactionRepositories transactionRepositories)
        {
            _transactionRepositories = transactionRepositories;
        }
        public IActionResult OnGet(int year = 0)
        {
            YearChoosing = (year != 0) ? year : DateTime.Now.Year;
            Revenue = _transactionRepositories.GetRevenue(YearChoosing);
            Years = _transactionRepositories.GetYears();
            return Page();
        }
    }
}
