using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace BookingVilla.Pages.ManageVillaPages
{
    public class AddNewVillaModel : PageModel
    {
        private readonly IVillaRepositories _villaRepositories;
        private readonly IHubContext<NewsHub> _hubContext;

        public AddNewVillaModel(IVillaRepositories villaRepositories, IHubContext<NewsHub> hubContext)
        {
            _villaRepositories = villaRepositories;
            _hubContext = hubContext;
        }

        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Desc { get; set; }
        [BindProperty] public IFormFile Image { get; set; }
        [BindProperty] public DateTime FromDate { get; set; }
        [BindProperty] public DateTime ToDate { get; set; }
        [BindProperty] public double Price { get; set; }
        [BindProperty] public int AmountPeople { get; set; }
        [BindProperty] public int AmountRoom { get; set; }
        public IActionResult OnPost()
        {
            // Xử lý upload hình ảnh (nếu có)
            string imagePath = null;
            if (Image != null)
            {
                var uploadsFolder = Path.Combine("wwwroot/img/Villa");
                Directory.CreateDirectory(uploadsFolder);
                imagePath = Path.Combine(uploadsFolder, Image.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyToAsync(fileStream);
                }
            }

            // Tạo đối tượng Villa mới
            var villa = new Villa
            {
                Name = Name,
                Describe = Desc,
                AmountOfPeople = AmountPeople,
                AmountOfRoom = AmountRoom,
                Status = true
            };
            int idvilla = _villaRepositories.AddVilla(villa);
            var pricevilla = new PriceVilla
            {
                FromDate = FromDate,
                ToDate = ToDate,
                PriceDay = Price,
                IdVilla = idvilla,
            };
            _villaRepositories.AddPriceVilla(pricevilla);
            var imagevilla = new ImageVilla
            {
                IdVilla = idvilla,
                Image = Image.FileName
            };
            _villaRepositories.AddImageVilla(imagevilla);
            _hubContext.Clients.All.SendAsync("ReceiveNewVilla", "Have New Villa!");
            TempData["SuccessMessage1"] = true ? "ok" : "fail";
            return RedirectToPage("/ManageVillaPages/ManageVilla");
        }
    }
}
