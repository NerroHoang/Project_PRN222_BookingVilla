using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BussinessObject;
using Repository;
namespace BookingVilla.Pages
{
    public class ShowVillaModel : PageModel
    {
        private readonly IVillaRepositories _villaRepository;

        public List<Villa> ListVilla { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public DateTime FromDate { get; set; } = DateTime.Today;

        [BindProperty]
        public DateTime ToDate { get; set; } = DateTime.Today.AddDays(2);

        [BindProperty]
        public int AmountRoom { get; set; } = 1;

        [BindProperty]
        public int NumOfPeople { get; set; } = 2;

        public ShowVillaModel(IVillaRepositories villaRepository)
        {
            _villaRepository = villaRepository;
        }

        public void OnGet()
        {
            ListVilla = _villaRepository.GetAvailableVillas(FromDate, ToDate, DateTime.Today, AmountRoom, NumOfPeople);
        }

        public void OnPost()
        {
            if (FromDate >= ToDate)
            {
                ErrorMessage = "Check-in date is not valid";
                ToDate = DateTime.Today.AddDays(2);
            }

            ListVilla = _villaRepository.GetAvailableVillas(FromDate, ToDate, DateTime.Today, AmountRoom, NumOfPeople);
        }
    }
}
