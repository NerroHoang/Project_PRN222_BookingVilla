using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServiceRepositories _serviceRepository;
        public List<Service> ListService { get; set; } = new();
        public IndexModel(IServiceRepositories serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public void OnGet()
        {
            ListService = _serviceRepository.GetAllService();
        }
    }
}
