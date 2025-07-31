using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ServiceDAO
    {
        private readonly BookingVillaPrnContext _context;

        public ServiceDAO(BookingVillaPrnContext context)
        {
            _context = context;
        }

        // Lấy tất cả dịch vụ
        public List<Service> GetAllService()
        {
            return _context.Services
                .Select(s => new Service
                {
                    IdService = s.IdService,
                    Name = s.Name,
                    Describe = s.Describe,
                    Image = s.Image,
                    Price = Convert.ToDouble(s.Price)
                })
                .ToList();
        }

        // Thêm dịch vụ mới
        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        // Lấy dịch vụ theo ID
        public Service GetServiceById(int idService)
        {
            return _context.Services
                .Select(s => new Service
                {
                    IdService = s.IdService,
                    Name = s.Name,
                    Describe = s.Describe,
                    Image = s.Image,
                    Price = Convert.ToDouble(s.Price)
                }).FirstOrDefault(s => s.IdService == idService);
        }

        // Lấy danh sách dịch vụ theo tên (sắp xếp theo tên)
        public List<Service> GetAllServiceByName()
        {
            return _context.Services.OrderBy(s => s.Name).ToList();
        }

        // Lấy danh sách dịch vụ theo giá (sắp xếp theo giá)
        public List<Service> GetAllServiceByPrice()
        {
            return _context.Services.OrderBy(s => s.Price).ToList();
        }

        // Tìm kiếm dịch vụ theo tên
        public List<Service> GetServiceByName(string name)
        {
            name = name.ToLower(); // Chuyển về chữ thường để tìm kiếm không phân biệt hoa thường
            return _context.Services
                .Select(s => new Service
                {
                    IdService = s.IdService,
                    Name = s.Name,
                    Describe = s.Describe,
                    Image = s.Image,
                    Price = Convert.ToDouble(s.Price)
                }).Where(s => s.Name.ToLower().Contains(name))
                .ToList();
        }

        // Tìm kiếm dịch vụ theo giá
        public List<Service> GetServiceByPrice(double price)
        {
            return _context.Services
                .Where(s => s.Price == price)
                .ToList();
        }

        // Chỉnh sửa dịch vụ
        public int EditService(Service service)
        {
            var existingService = GetServiceById(service.IdService);
            if (existingService != null)
            {
                existingService.Name = service.Name;
                existingService.Describe = service.Describe;
                existingService.Image = service.Image;
                existingService.Price = service.Price;
                _context.Update(existingService);
                return _context.SaveChanges();
            }
            return 0;
        }

        // Xóa dịch vụ
        public void DeleteService(int idService)
        {
            var service = _context.Services.Find(idService);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }
        public List<Service> GetAllServicePagination(int index)
        {
            {
                return _context.Services.Select(s => new Service
                {
                    IdService = s.IdService,
                    Name = s.Name,
                    Describe = s.Describe,
                    Image = s.Image,
                    Price = Convert.ToDouble(s.Price)
                })
                    .OrderByDescending(a => a.IdService) // Sắp xếp theo Status DESC
                    .Skip((index - 1) * 4) // OFFSET
                    .Take(4)
                    .ToList();
            }
        }
        public int GetNumberTotalService()
        {
            return _context.Services.Count();
        }

        public int GetNumberService()
        {
            int total = _context.Services.Count();
            int countPage = total / 4;
            if (total % 4 != 0)
            {
                countPage++;
            }
            return countPage;
        }
    }
}

