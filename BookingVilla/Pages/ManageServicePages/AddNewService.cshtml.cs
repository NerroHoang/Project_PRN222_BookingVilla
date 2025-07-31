using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repository;

namespace BookingVilla.Pages.ManageServicePages
{
    public class AddNewServiceModel : PageModel
    {
        private readonly IServiceRepositories _serviceRepositories;

        public AddNewServiceModel(IServiceRepositories serviceRepositories)
        {
            _serviceRepositories = serviceRepositories;
        }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Desc { get; set; }
        [BindProperty] public IFormFile Image { get; set; }
        [BindProperty] public double Price { get; set; }
        public IActionResult OnPost()
        {
            // Xử lý upload hình ảnh (nếu có)
            string imagePath = null;
            if (Image != null)
            {
                var uploadsFolder = Path.Combine("wwwroot/img/Service");
                Directory.CreateDirectory(uploadsFolder);
                imagePath = Path.Combine(uploadsFolder, Image.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyToAsync(fileStream);
                }
            }
            var service = new Service
            {
                Name = Name,
                Describe = Desc,
                Price = Price,
                Image = Image.FileName
            };
            _serviceRepositories.AddService(service);
            TempData["SuccessMessage1"] = true ? "ok" : "fail";
            return RedirectToPage("/ManageServicePages/ManageService");
        }
    }
}
