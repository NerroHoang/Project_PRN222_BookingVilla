using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Repository
{
    public class VillaRepositories : IVillaRepositories
    {

        private readonly VillaDAO villaDAO;
        public VillaRepositories(BookingVillaPrnContext context)
        {
            villaDAO = new VillaDAO(context);
        }

        public int AddImageVilla(ImageVilla image) => villaDAO.AddImageVilla(image);

        public void AddPriceVilla(PriceVilla priceVilla) => villaDAO.AddPriceVilla(priceVilla);

        public int AddVilla(Villa villa) => villaDAO.AddVilla(villa);

        public List<Villa> GetAllVillasByPeople(int index) => villaDAO.GetAllVillasByPeople(index);

        public List<Villa> GetAllVillasByPrice(int index) => villaDAO.GetAllVillasByPrice(index);

        public List<Villa> GetAllVillasByRoom(int index) => villaDAO.GetAllVillasByRoom(index);

        public List<Villa> GetAvailableVillas(DateTime from, DateTime to, DateTime currentDate, int amountRoom, int amountPeople) => villaDAO.GetAvailableVillas(from, to, currentDate, amountRoom, amountPeople);

        public int GetNumberTotalVilla() => villaDAO.GetNumberTotalVilla();

        public int GetNumberVilla() => villaDAO.GetNumberVilla();

        public PriceVilla GetPriceVillaByIdVilla(int id) => villaDAO.GetPriceVillaByIdVilla(id);
        public Villa GetVillaById(int id, DateTime currentDate) => villaDAO.GetVillaById(id, currentDate);

        public bool GetVillaConflict(int id) => villaDAO.GetVillaConflict(id);

        public List<int> GetVillaConflictIds(DateTime from, DateTime to) => GetVillaConflictIds(from, to);

        public List<Villa> SearchByName(string name) => villaDAO.SearchByName(name);

        public void UpdatePriceVilla(PriceVilla priceVilla) => villaDAO.UpdatePriceVilla(priceVilla);

        public void UpdateVilla(Villa villa) => villaDAO.UpdateVilla(villa);
    }
}
