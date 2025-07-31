using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageServicePages
{
    public class UpdateServiceModel : PageModel
    {
        private readonly IServiceRepositories _serviceRepositories;

        public UpdateServiceModel(IServiceRepositories serviceRepositories)
        {
            _serviceRepositories = serviceRepositories;
        }
        [BindProperty] public int Id { get; set; }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Desc { get; set; }
        [BindProperty] public string Img { get; set; }
        [BindProperty] public IFormFile Image { get; set; }
        [BindProperty] public double Price { get; set; }
        public void OnGet(int id)
        {
            var service = _serviceRepositories.GetServiceById(id);
            Id = service.IdService;
            Name = service.Name;
            Desc = service.Describe;
            Img = service.Image;
            Price = service.Price;
        }
        public IActionResult OnPost()
        {
            // Xử lý upload hình ảnh (nếu có)
            string imagePath = null;
            var service = _serviceRepositories.GetServiceById(Id);
            if (Image != null)
            {
                var uploadsFolder = Path.Combine("wwwroot/img/Service");
                Directory.CreateDirectory(uploadsFolder);
                imagePath = Path.Combine(uploadsFolder, Image.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyToAsync(fileStream);
                }
                service.Image = Image.FileName;
            }
            else
            {
                service.Image = service.Image;
            }
            service.Name = Name;
            service.Describe = Desc;
            service.Price = Price;
            _serviceRepositories.EditService(service);
            TempData["SuccessMessage2"] = true ? "ok" : "fail";
            return RedirectToPage("/ManageServicePages/ManageService");
        }
    }
}
