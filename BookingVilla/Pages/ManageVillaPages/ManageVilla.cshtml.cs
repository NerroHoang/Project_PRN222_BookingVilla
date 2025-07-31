using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageVillaPages
{
    public class ManageVillaModel : PageModel
    {
        private readonly IVillaRepositories _villaRepo;

        public List<Villa> Villas { get; set; }
        public int TotalVilla { get; set; }
        public int NumberPage { get; set; }
        public int Index { get; set; }
        public string Choice { get; set; }
        [BindProperty]
        public string Search { get; set; }

        public ManageVillaModel(IVillaRepositories villaRepo)
        {
            _villaRepo = villaRepo;
        }

        public void OnGet(int index = 1, string choice = null)
        {
            Choice = choice ?? Choice;
            Index = index;

            Villas = Choice switch
            {
                "people" => _villaRepo.GetAllVillasByPeople(index),
                "room" => _villaRepo.GetAllVillasByRoom(index),
                _ => _villaRepo.GetAllVillasByPrice(index)
            };

            TotalVilla = _villaRepo.GetNumberTotalVilla();
            NumberPage = _villaRepo.GetNumberVilla();
        }
        public IActionResult OnGetDelete(int id, string choice)
        {
            Choice = choice ?? Choice;
            Index = 1;
            bool result = _villaRepo.GetVillaConflict(id);
            TempData["SuccessMessage"] = result ? "ok" : "fail";

            Villas = Choice switch
            {
                "people" => _villaRepo.GetAllVillasByPeople(Index),
                "room" => _villaRepo.GetAllVillasByRoom(Index),
                _ => _villaRepo.GetAllVillasByPrice(Index)
            };

            TotalVilla = _villaRepo.GetNumberTotalVilla();
            NumberPage = _villaRepo.GetNumberVilla();
            return RedirectToPage("/ManageVillaPages/ManageVilla", new { choice });
        }
        public IActionResult OnPost()
        {
            Villas = _villaRepo.SearchByName(Search);
            Index = 1;
            TotalVilla = Villas.Count;
            NumberPage = (TotalVilla / 4) + (TotalVilla % 4 > 0 ? 1 : 0);
            return Page();
        }
    }

}
