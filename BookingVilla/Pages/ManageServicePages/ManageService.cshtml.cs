using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageServicePages
{
    public class ManageServiceModel : PageModel
    {
        private readonly IServiceRepositories _serviceRepositories;
        public ManageServiceModel(IServiceRepositories serviceRepositories)
        {
            _serviceRepositories = serviceRepositories;
        }
        public List<Service> Services { get; set; }
        [BindProperty]
        public string Search { get; set; }
        public int TotalServices { get; set; }
        public int NumberPage { get; set; }
        public int Index { get; set; } = 1;
        public void OnGet(int index = 1)
        {
            Index = index;

            // Lấy danh sách tài khoản
            Services = _serviceRepositories.GetAllServicePagination(index);

            // Tính tổng số tài khoản
            TotalServices = _serviceRepositories.GetNumberTotalService();

            // Tính số trang
            NumberPage = _serviceRepositories.GetNumberService();
        }
        public IActionResult OnGetDelete(int id)
        {
            _serviceRepositories.DeleteService(id);
            bool result = true;
            TempData["SuccessMessage"] = result ? "ok" : "fail";
            return RedirectToPage("/ManageServicePages/ManageService");
        }
        public IActionResult OnPost()
        {
            Index = 1;
            Services = _serviceRepositories.GetServiceByName(Search);
            TotalServices = Services.Count;
            NumberPage = (TotalServices / 4) + (TotalServices % 4 > 0 ? 1 : 0);
            return Page();
        }
    }
}
