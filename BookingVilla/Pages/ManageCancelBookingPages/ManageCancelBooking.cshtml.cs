using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace BookingVilla.Pages.ManageCancelBookingPages
{
    public class ManageCancelBookingModel : PageModel
    {
        private readonly ICancelBookingRepositories _cancelBookingRepository;
        [BindProperty]
        public List<ManageCancelBooking> CancelBookings { get; set; }
        [BindProperty]
        public int NumberTotal { get; set; }
        [BindProperty]
        public int NumberPage { get; set; }
        [BindProperty]
        public int Index { get; set; } = 1;
        [BindProperty]
        public string Choice { get; set; } = "pending";
        [BindProperty]
        public string Search { get; set; }

        public ManageCancelBookingModel(ICancelBookingRepositories cancelBookingRepository)
        {
            _cancelBookingRepository = cancelBookingRepository;
        }

        public IActionResult OnGet(string choice, int? index)
        {
            Index = index ?? 1;
            Choice = string.IsNullOrEmpty(choice) ? "pending" : choice;

            switch (Choice)
            {
                case "pending":
                    CancelBookings = _cancelBookingRepository.GetAllCancelBookingsPagination("Pending", Index, 5);
                    NumberTotal = _cancelBookingRepository.GetTotalCancelBookingCountByStatus("Pending");
                    NumberPage = _cancelBookingRepository.GetCancelBookingPageCountByStatus("Pending", 5);
                    break;
                case "rejected":
                    CancelBookings = _cancelBookingRepository.GetAllCancelBookingsPagination("Rejected", Index, 5);
                    NumberTotal = _cancelBookingRepository.GetTotalCancelBookingCountByStatus("Rejected");
                    NumberPage = _cancelBookingRepository.GetCancelBookingPageCountByStatus("Rejected", 5);
                    break;
                case "approved":
                    CancelBookings = _cancelBookingRepository.GetAllCancelBookingsPagination("Approved", Index, 5);
                    NumberTotal = _cancelBookingRepository.GetTotalCancelBookingCountByStatus("Approved");
                    NumberPage = _cancelBookingRepository.GetCancelBookingPageCountByStatus("Approved", 5);
                    break;
                case "all":
                    CancelBookings = _cancelBookingRepository.GetAllCancelBookingsPaginationAll(Index, 5);
                    NumberTotal = _cancelBookingRepository.GetTotalCancelBookingCount();
                    NumberPage = _cancelBookingRepository.GetCancelBookingPageCount(5);
                    break;
                default:
                    return RedirectToPage("/Error");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Search))
            {
                CancelBookings = _cancelBookingRepository.GetAllCancelBookingsPaginationAll(Index, 5);
                NumberTotal = _cancelBookingRepository.GetTotalCancelBookingCount();
                NumberPage = _cancelBookingRepository.GetCancelBookingPageCount(5);
            }
            else
            {
                CancelBookings = _cancelBookingRepository.SearchCancelBookings(Search);
            }
            return Page();
        }
        public IActionResult OnGetApproveCancelBooking(int idcancelbooking)
        {
            _cancelBookingRepository.UpdateStatusCancelBooking(idcancelbooking, "Approved");

            return RedirectToPage("ManageCancelBooking");
        }

        public IActionResult OnGetRejectCancelBooking(int idcancelbooking)
        {
            _cancelBookingRepository.UpdateStatusCancelBooking(idcancelbooking, "Rejected");

            return RedirectToPage("ManageCancelBooking");
        }
    }
}
