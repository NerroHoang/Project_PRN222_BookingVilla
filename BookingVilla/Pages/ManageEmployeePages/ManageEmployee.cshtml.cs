using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace BookingVilla.Pages.ManageEmployeePages
{
    public class ManageEmployeeModel : PageModel
    {
        private readonly IEmployeeRepositories _employeeRepositories;
        [BindProperty]
        public string Search { get; set; }
        public List<Employee> Employees { get; set; }
        public ManageEmployeeModel(IEmployeeRepositories employeeRepositories)
        {
            _employeeRepositories = employeeRepositories;
        }
        public void OnGet()
        {
            Employees = _employeeRepositories.GetAllEmployees();
        }
        public IActionResult OnPost()
        {
            Employees = _employeeRepositories.GetEmployeesByName(Search);
            return Page();
        }
    }
}
