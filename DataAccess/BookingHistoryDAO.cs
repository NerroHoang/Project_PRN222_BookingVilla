using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class BookingHistoryDAO
{
    private readonly BookingVillaPrnContext _context;

    public BookingHistoryDAO(BookingVillaPrnContext context)
    {
        _context = context;
    }

    public List<BookingHistory> GetAllBookingHistoryStatus(int idCustomer)
    {
        return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer)
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .Where(b => b.Status != "Rejected" && b.Status != null)
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList();
    }

    public List<BookingHistory> GetAllBookingHistoryNoStatus(int idCustomer)
    {
        return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer &&
                        (!_context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline) ||
                         _context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline && ca.Status == "Rejected")))
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList();
    }
    public List<BookingHistory> GetAllBookingHistoryNoStatusPagination(int idCustomer, int index)
    {
        {
            return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer &&
                        (!_context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline) ||
                         _context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline && ca.Status == "Rejected")))
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .OrderByDescending(b => b.IdBookingOnline)
                .Skip((index - 1) * 4) // OFFSET
                .Take(4)
                .ToList();
        }
    }
    public int GetNumberTotalBookingHistoryNoStatus(int idCustomer)
    {
        return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer &&
                        (!_context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline) ||
                         _context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline && ca.Status == "Rejected")))
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList().Count();
    }
    public int GetNumberBookingHistoryNoStatus(int idCustomer)
    {
        int total = _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer &&
                        (!_context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline) ||
                         _context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline && ca.Status == "Rejected")))
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList().Count();
        int countPage = total / 4;
        if (total % 4 != 0)
        {
            countPage++;
        }
        return countPage;
    }
    public List<BookingHistory> GetAllBookingHistoryStatusPagination(int idCustomer, int index)
    {
        {
            return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer &&
                        (!_context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline) ||
                         _context.CancelBookings.Any(ca => ca.IdBookingOnline == b.IdBookingOnline && ca.Status == "Rejected")))
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .Where(b => b.Status != "Rejected" && b.Status != null)
            .OrderByDescending(b => b.IdBookingOnline)
                .Skip((index - 1) * 6) // OFFSET
                .Take(6)
                .ToList();
        }
    }
    public int GetNumberTotalBookingHistoryStatus(int idCustomer)
    {
        return _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer)
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .Where(b => b.Status != "Rejected" && b.Status != null)
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList().Count();
    }

    public int GetNumberBookingHistoryStatus(int idCustomer)
    {
        int total = _context.BookingOnlines
            .Where(b => b.IdCustomer == idCustomer)
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .Where(b => b.Status != "Rejected" && b.Status != null)
            .OrderByDescending(b => b.IdBookingOnline)
            .ToList().Count();
        int countPage = total / 6;
        if (total % 6 != 0)
        {
            countPage++;
        }
        return countPage;
    }

    public BookingHistory GetBookingHistoryById(int idBooking)
    {
        return _context.BookingOnlines
            .Where(b => b.IdBookingOnline == idBooking)
            .Select(b => new BookingHistory
            {
                IdBookingOnline = b.IdBookingOnline,
                CheckinDate = b.CheckinDate,
                CheckoutDate = b.CheckoutDate,
                AmountOfPeople = b.AmountOfPeople,
                PriceBooking = b.PriceBooking,
                IdCustomer = b.IdCustomer,
                IdVilla = b.IdVilla,
                NameVilla = b.IdVillaNavigation.Name,
                ServicesIncluded = _context.AddServices
                    .Where(a => a.IdBookingOnline == b.IdBookingOnline)
                    .Select(a => a.IdServiceNavigation.Name + " x" + a.Quantity)
                    .ToList(),
                Status = _context.CancelBookings
                    .Where(ca => ca.IdBookingOnline == b.IdBookingOnline)
                    .Select(ca => ca.Status)
                    .FirstOrDefault()
            })
            .FirstOrDefault();
    }
}
