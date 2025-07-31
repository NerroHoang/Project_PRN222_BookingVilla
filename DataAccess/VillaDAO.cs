using BussinessObject;
using Microsoft.EntityFrameworkCore;

public class VillaDAO
{
    private readonly BookingVillaPrnContext _context;

    public VillaDAO(BookingVillaPrnContext context)
    {
        _context = context;
    }

    public List<Villa> GetAllVillas(DateTime currentDate, int amountRoom, int amountPeople)
    {
        return _context.Villas
            .Where(v => v.Status == true &&
                        v.AmountOfRoom >= amountRoom &&
                        v.AmountOfPeople >= amountPeople)
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfPeople = v.AmountOfPeople,
                AmountOfRoom = v.AmountOfRoom,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDate >= p.FromDate && currentDate <= p.ToDate)
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(), // Lấy giá đầu tiên phù hợp
                ImageVillas = v.ImageVillas.ToList() // Giữ nguyên kiểu List<ImageVilla>
            })
            .OrderBy(v => v.AmountOfRoom)
            .ToList();
    }
    public List<int> GetVillaConflictIds(DateTime from, DateTime to)
    {
        return _context.BookingOnlines
            .Include(b => b.CancelBookings) // Đảm bảo dữ liệu của CancelBooking được load
            .Where(b => b.CheckoutDate > from && b.CheckinDate < to &&
                        (!b.CancelBookings.Any() || b.CancelBookings.Any(c => c.Status == "Rejected")))
            .Select(b => b.IdVilla)
            .Distinct()
            .ToList();
    }


    public List<Villa> GetAvailableVillas(DateTime from, DateTime to, DateTime currentDate, int amountRoom, int amountPeople)
    {
        var allVillas = GetAllVillas(currentDate, amountRoom, amountPeople);
        var conflictVillaIds = GetVillaConflictIds(from, to);

        return allVillas.Where(v => !conflictVillaIds.Contains(v.IdVilla)).ToList();
    }

    public Villa GetVillaById(int id, DateTime currentDate)
    {
        return _context.Villas
            .Where(v => v.IdVilla == id && v.Status == true)
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfPeople = v.AmountOfPeople,
                AmountOfRoom = v.AmountOfRoom,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDate >= p.FromDate && currentDate <= p.ToDate)
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(), // Lấy giá phù hợp với ngày hiện tại
                ImageVillas = v.ImageVillas.ToList() // Giữ nguyên kiểu List<ImageVilla>
            })
            .FirstOrDefault();
    }


    public PriceVilla GetPriceVillaByIdVilla(int id)
    {
        var currentDate = DateTime.Now;
        return _context.PriceVillas
            .Where(p => currentDate >= p.FromDate && currentDate <= p.ToDate)
            .FirstOrDefault(p => p.IdVilla == id);
    }

    public List<Villa> GetAllVillas()
    {
        DateTime currentDay = DateTime.Today;
        return _context.Villas
            .Include(v => v.PriceVillas)
            .Where(v => v.Status == true && v.PriceVillas.Any(p => currentDay >= p.FromDate && currentDay <= p.ToDate))
            .OrderBy(v => v.AmountOfRoom)
            .ToList();
    }

    public void UpdateVillaStatus(int id)
    {
        var villa = _context.Villas.Find(id);
        if (villa != null)
        {
            villa.Status = true;
            _context.SaveChanges();
        }
    }

    public void UpdateVillaImage(int id, string img)
    {
        var image = _context.ImageVillas.Find(id);
        if (image != null)
        {
            image.Image = img;
            _context.SaveChanges();
        }
    }

    public List<Villa> SearchByName(string name)
    {
        var currentDate = DateTime.Today;
        return _context.Villas
            .Where(v => v.Status == true &&
                        v.Name.ToLower().Contains(name.ToLower())) // Tìm kiếm theo tên
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfPeople = v.AmountOfPeople,
                AmountOfRoom = v.AmountOfRoom,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDate >= p.FromDate && currentDate <= p.ToDate)
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(), // Lấy giá đầu tiên phù hợp
                ImageVillas = v.ImageVillas.ToList() // Giữ nguyên kiểu List<ImageVilla>
            })
            .ToList();
    }

    public List<Villa> GetAllVillasByPrice(int index)
    {
        DateTime currentDay = DateTime.Today;
        int pageSize = 4;

        return _context.Villas
            .Include(v => v.PriceVillas)
            .Where(v => v.Status == true && v.PriceVillas.Any(p => currentDay >= p.FromDate && currentDay <= p.ToDate))
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfRoom = v.AmountOfRoom,
                AmountOfPeople = v.AmountOfPeople,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDay >= p.FromDate && currentDay <= p.ToDate)
                    .OrderByDescending(p => p.FromDate) // Ưu tiên giá mới nhất trong khoảng
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(),
                ImageVillas = v.ImageVillas.ToList()
            })
            .OrderByDescending(v => v.Price)
            .Skip((index - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToList();
    }
    public List<Villa> GetAllVillasByPeople(int index)
    {
        DateTime currentDay = DateTime.Today;
        int pageSize = 4;

        return _context.Villas
            .Include(v => v.PriceVillas)
            .Where(v => v.Status == true && v.PriceVillas.Any(p => currentDay >= p.FromDate && currentDay <= p.ToDate))
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfRoom = v.AmountOfRoom,
                AmountOfPeople = v.AmountOfPeople,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDay >= p.FromDate && currentDay <= p.ToDate)
                    .OrderByDescending(p => p.FromDate) // Ưu tiên giá mới nhất trong khoảng
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(),
                ImageVillas = v.ImageVillas.ToList()
            })
            .OrderByDescending(v => v.AmountOfPeople)
            .Skip((index - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToList();
    }
    public List<Villa> GetAllVillasByRoom(int index)
    {
        DateTime currentDay = DateTime.Today;
        int pageSize = 4;

        return _context.Villas
            .Include(v => v.PriceVillas)
            .Where(v => v.Status == true && v.PriceVillas.Any(p => currentDay >= p.FromDate && currentDay <= p.ToDate))
            .Select(v => new Villa
            {
                IdVilla = v.IdVilla,
                Name = v.Name,
                Describe = v.Describe,
                AmountOfRoom = v.AmountOfRoom,
                AmountOfPeople = v.AmountOfPeople,
                Status = v.Status,
                Price = v.PriceVillas
                    .Where(p => currentDay >= p.FromDate && currentDay <= p.ToDate)
                    .OrderByDescending(p => p.FromDate) // Ưu tiên giá mới nhất trong khoảng
                    .Select(p => p.PriceDay)
                    .FirstOrDefault(),
                ImageVillas = v.ImageVillas.ToList()
            })
            .OrderByDescending(v => v.AmountOfRoom)
            .Skip((index - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToList();
    }
    public ImageVilla GetImageByVillaId(int villaId)
    {
        return _context.ImageVillas
            .Where(i => i.IdVilla == villaId)
            .FirstOrDefault();
    }
    public int AddVilla(Villa villa)
    {
        _context.Villas.Add(villa);
        _context.SaveChanges();
        return villa.IdVilla;
    }
    public void AddPriceVilla(PriceVilla priceVilla)
    {
        _context.PriceVillas.Add(priceVilla);
        _context.SaveChanges();
    }
    public void UpdateVilla(Villa villa)
    {
        var existingVilla = _context.Villas.Find(villa.IdVilla);
        if (existingVilla != null)
        {
            existingVilla.Name = villa.Name;
            existingVilla.Describe = villa.Describe;
            existingVilla.AmountOfPeople = villa.AmountOfPeople;
            existingVilla.AmountOfRoom = villa.AmountOfRoom;
            _context.Update(existingVilla);
            _context.SaveChanges();
        }
    }

    public void UpdatePriceVilla(PriceVilla priceVilla)
    {
        var existingPrice = _context.PriceVillas
            .FirstOrDefault(p => p.IdVilla == priceVilla.IdVilla);

        if (existingPrice != null)
        {
            existingPrice.FromDate = priceVilla.FromDate;
            existingPrice.ToDate = priceVilla.ToDate;
            existingPrice.PriceDay = priceVilla.PriceDay;
            _context.Update(existingPrice);
            _context.SaveChanges();
        }
    }

    public bool GetVillaConflict(int id)
    {
        bool hasConflict = _context.BookingOnlines
            .Include(b => b.CancelBookings)
            .Include(b => b.IdVillaNavigation)
            .Any(b => (b.CancelBookings == null || b.CancelBookings.Any(c => c.Status == "Rejected")) && b.IdVillaNavigation.IdVilla == id);

        if (hasConflict)
        {
            return false;
        }
        else
        {
            var villa = GetVillaById(id, DateTime.Now);
            if (villa != null)
            {
                villa.Status = false;
                _context.Villas.Update(villa);
                _context.SaveChanges();
            }
            return true;
        }
    }

    public bool DeleteVillaById(int id)
    {
        var villa = GetVillaById(id, DateTime.Now);
        if (villa != null)
        {
            villa.Status = false;
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public int GetNumberTotalVilla()
    {
        return _context.Villas.Count(v => v.Status == true);
    }

    public int GetNumberVilla()
    {
        int total = _context.Villas.Count(v => v.Status == true);
        return (int)Math.Ceiling(total / 4.0);
    }

    public List<Villa> GetVillasPagination(int index)
    {
        DateTime currentDay = DateTime.Now;
        return _context.Villas
            .Include(v => v.PriceVillas)
            .Where(v => v.PriceVillas.Any(p => currentDay >= p.FromDate && currentDay <= p.ToDate))
            .OrderBy(v => v.IdVilla)
            .Skip((index - 1) * 4)
            .Take(4)
            .ToList();
    }

    public double GetAveragePoint(int idVilla)
    {
        return _context.Feedbacks
            .Where(f => f.IdVilla == idVilla)
            .Average(f => (double?)f.Point) ?? 0;
    }

    public void UpdatePointVilla(int idVilla, double averagePoint)
    {
        var villa = _context.Villas.Find(idVilla);
        if (villa != null)
        {
            villa.Point = averagePoint;
            _context.SaveChanges();
        }
    }

    public int AddImageVilla(ImageVilla image)
    {
        _context.ImageVillas.Add(image);
        _context.SaveChanges();
        return image.IdImgVilla;
    }

    public List<ImageVilla> GetAllImageVillas()
    {
        return _context.ImageVillas.ToList();
    }

    public void DeleteImageByIdVilla(int id)
    {
        var image = _context.ImageVillas.Find(id);
        if (image != null)
        {
            _context.ImageVillas.Remove(image);
            _context.SaveChanges();
        }
    }

    public void UpdateImageByIdVilla(int id, string img)
    {
        var image = _context.ImageVillas.Find(id);
        if (image != null)
        {
            image.Image = img;
            _context.SaveChanges();
        }
    }

    public List<ImageVilla> GetImageVillaByIdVilla(int idVilla)
    {
        return _context.ImageVillas
            .Where(img => img.IdVilla == idVilla)
            .ToList();
    }

    public ImageVilla GetImageByIdVilla(int idVilla)
    {
        return _context.ImageVillas
            .FirstOrDefault(img => img.IdVilla == idVilla);
    }
}
