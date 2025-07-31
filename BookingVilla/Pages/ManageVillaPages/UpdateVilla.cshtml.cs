using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Repository;

namespace BookingVilla.Pages.ManageVillaPages
{
    public class UpdateVillaModel : PageModel
    {
        private readonly IVillaRepositories _villaRepositories;

        public UpdateVillaModel(IVillaRepositories villaRepositories)
        {
            _villaRepositories = villaRepositories;
        }

        [BindProperty] public int Id { get; set; }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Desc { get; set; }
        [BindProperty] public IFormFile Image { get; set; }
        [BindProperty] public DateTime FromDate { get; set; }
        [BindProperty] public DateTime ToDate { get; set; }
        [BindProperty] public double Price { get; set; }
        [BindProperty] public int AmountPeople { get; set; }
        [BindProperty] public int AmountRoom { get; set; }
        public void OnGet(int id)
        {
            var villa = _villaRepositories.GetVillaById(id, DateTime.Now);
            var pricevilla = _villaRepositories.GetPriceVillaByIdVilla(id);
            Id = villa.IdVilla;
            Name = villa.Name;
            Desc = villa.Describe;
            FromDate = pricevilla.FromDate;
            ToDate = pricevilla.ToDate;
            Price = villa.Price;
            AmountPeople = villa.AmountOfPeople;
            AmountRoom = villa.AmountOfRoom;
        }
        public IActionResult OnPost()
        {
            var villa = _villaRepositories.GetVillaById(Id, DateTime.Now);
            var pricevilla = _villaRepositories.GetPriceVillaByIdVilla(Id);
            villa.Name = Name;
            villa.Describe = Desc;
            villa.AmountOfPeople = AmountPeople;
            villa.AmountOfRoom = AmountRoom;
            _villaRepositories.UpdateVilla(villa);
            pricevilla.FromDate = FromDate;
            pricevilla.ToDate = ToDate;
            pricevilla.PriceDay = Price;
            _villaRepositories.UpdatePriceVilla(pricevilla);
            TempData["SuccessMessage2"] = true ? "ok" : "fail";
            return RedirectToPage("/ManageVillaPages/ManageVilla");
        }
    }
}
