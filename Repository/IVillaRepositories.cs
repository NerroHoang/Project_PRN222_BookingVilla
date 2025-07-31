using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject;

namespace Repository
{
    public interface IVillaRepositories
    {
        public List<int> GetVillaConflictIds(DateTime from, DateTime to);
        public List<Villa> GetAvailableVillas(DateTime from, DateTime to, DateTime currentDate, int amountRoom, int amountPeople);
        public Villa GetVillaById(int id, DateTime currentDate);
        public PriceVilla GetPriceVillaByIdVilla(int id);
        public List<Villa> GetAllVillasByPrice(int index);
        public List<Villa> GetAllVillasByPeople(int index);
        public List<Villa> GetAllVillasByRoom(int index);
        public int GetNumberTotalVilla();
        public int GetNumberVilla();
        public bool GetVillaConflict(int id);
        public int AddVilla(Villa villa);
        public int AddImageVilla(ImageVilla image);
        public void AddPriceVilla(PriceVilla priceVilla);
        public List<Villa> SearchByName(string name);
        public void UpdateVilla(Villa villa);
        public void UpdatePriceVilla(PriceVilla priceVilla);
    }
}
