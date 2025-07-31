using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using System.Globalization;
using System.Text.Json;

namespace BookingVilla.Pages.ManageBookingPages
{
    public class BookingModel : PageModel
    {
        private readonly IVillaRepositories _villaRepository;
        private readonly IServiceRepositories _serviceRepository;
        private readonly IBookingRepositories _bookingRepository;
        private readonly ITransactionRepositories _transactionRepositories;

        public Villa Villa { get; set; }
        public List<Service> ListService { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;
        public double Price { get; set; }
        public double PriceBase { get; set; }

        [BindProperty]
        public int NumOfPeople { get; set; }

        [BindProperty]
        public DateTime FromDate { get; set; } = DateTime.Today;

        [BindProperty]
        public DateTime ToDate { get; set; } = DateTime.Today.AddDays(1);

        public BookingModel(IVillaRepositories villaRepository, IServiceRepositories serviceRepository, IBookingRepositories bookingRepositories, ITransactionRepositories transactionRepositories)
        {
            _villaRepository = villaRepository;
            _serviceRepository = serviceRepository;
            _bookingRepository = bookingRepositories;
            _transactionRepositories = transactionRepositories;
        }

        public IActionResult OnGet(int id, string fromDate, string toDate, int numOfPeople)
        {
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            if (string.IsNullOrEmpty(userInfoJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (!DateTime.TryParse(fromDate, out DateTime startDate) || !DateTime.TryParse(toDate, out DateTime endDate))
            {
                ErrorMessage = "Invalid date format.";
                return Page();
            }

            if (endDate <= startDate)
            {
                ErrorMessage = "Check-in date is not valid.";
                return Page();
            }

            Villa = _villaRepository.GetVillaById(id, DateTime.Today);
            if (Villa == null)
            {
                ErrorMessage = "Villa not found.";
                return Page();
            }

            long daysBetween = (endDate - startDate).Days;
            PriceBase = Villa.Price;
            Price = Villa.Price * daysBetween;
            ListService = _serviceRepository.GetAllService();
            FromDate = startDate;
            ToDate = endDate;
            NumOfPeople = numOfPeople;
            return Page();
        }

        public IActionResult OnPost(int idvilla, string fromDate, string toDate, int numofpeople)
        {
            // Lấy thông tin người dùng từ session
            var userInfoJson = HttpContext.Session.GetString("UserInfo");
            var userInfo = JsonSerializer.Deserialize<dynamic>(userInfoJson);
            int customerId = (int)userInfo.GetProperty("IdCustomer").GetInt32();

            // Kiểm tra ngày hợp lệ
            if (!DateTime.TryParse(fromDate, out DateTime startDate) || !DateTime.TryParse(toDate, out DateTime endDate))
            {
                ErrorMessage = "Định dạng ngày không hợp lệ.";
                return Page();
            }
            if (endDate <= startDate)
            {
                ErrorMessage = "Ngày trả phòng phải sau ngày nhận phòng.";
                return Page();
            }

            // Lấy tổng giá từ input "total-price"
            var totalString = Request.Form["total"].ToString().Replace(".", "").Replace(",", ".");
            if (!double.TryParse(totalString, NumberStyles.Any, CultureInfo.InvariantCulture, out double totalPrice))
            {
                ErrorMessage = "Lỗi khi lấy tổng giá.";
                return Page();
            }
            // Tạo đơn đặt phòng
            var newBooking = new BookingOnline
            {
                IdCustomer = customerId,
                IdVilla = idvilla,
                CheckinDate = startDate,
                CheckoutDate = endDate,
                AmountOfPeople = numofpeople,
                PriceBooking = totalPrice
            };
            int idbooking = _bookingRepository.AddBooking(newBooking);
            ListService = _serviceRepository.GetAllService();
            foreach (var service in ListService)
            {
                // Lấy giá trị checkbox của dịch vụ
                string serviceValue = Request.Form[$"service_{service.IdService}"];

                if (!string.IsNullOrEmpty(serviceValue)) // Nếu dịch vụ được chọn
                {
                    // Lấy số lượng (nếu có)
                    string quantityStr = Request.Form[$"quantity_{service.IdService}"];
                    int quantity = !string.IsNullOrEmpty(quantityStr) && int.TryParse(quantityStr, out int qty) && qty > 0 ? qty : 1;

                    // Thêm vào danh sách
                    var newAddService = new AddService
                    {
                        IdBookingOnline = idbooking,
                        IdService = service.IdService,
                        Quantity = quantity,
                    };
                    _bookingRepository.AddServiceBooking(idbooking, newAddService);
                }
            }
            var newTrans = new Transaction
            {
                Date = DateTime.Now,
                IdTransactions = idbooking,
                Price = totalPrice,
            };
            _transactionRepositories.AddTransaction(newTrans);
            TempData["SuccessMessage"] = "Booking Success!";
            return RedirectToPage("/ShowVilla");
        }
    }
}
