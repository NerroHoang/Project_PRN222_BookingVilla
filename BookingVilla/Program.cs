using BussinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace BookingVilla
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Thi?t l?p DbContext v?i SQL Server
            builder.Services.AddDbContext<BookingVillaPrnContext>(options =>
                options.UseSqlServer("BookingVillaPRN"));

            // ??ng ký Repository và DAO
            builder.Services.AddScoped<IVillaRepositories, VillaRepositories>();
            builder.Services.AddScoped<IServiceRepositories, ServiceRepositories>();
            builder.Services.AddScoped<IAccountRepositories, AccountRepositories>();
            builder.Services.AddScoped<IBookingRepositories, BookingRepositories>();
            builder.Services.AddScoped<IBookingHistoryRepositories, BookingHistoryRepositories>();
            builder.Services.AddScoped<ICancelBookingRepositories, CancelBookingRepositories>();
            builder.Services.AddScoped<IEmployeeRepositories, EmployeeRepositories>();
            builder.Services.AddScoped<ITransactionRepositories, TransactionRepositories>();
            builder.Services.AddScoped<ICustomerRepositories, CustomerRepository>();

            builder.Services.AddScoped<EmployeeDAO>();

            // C?u hình Session
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // C?u hình Razor Pages và CORS
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // C?u hình Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Ph?c v? file t?nh
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapHub<NewsHub>("/newsHub");

            // C?u hình Endpoint cho Razor Pages
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.Run();
        }
    }
}
