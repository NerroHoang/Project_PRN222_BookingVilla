using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IServiceRepositories
    {
        List<Service> GetAllService();
        void AddService(Service service);
        Service GetServiceById(int idService);
        List<Service> GetServiceByName(string name);
        int EditService(Service service);
        void DeleteService(int idService);
        public List<Service> GetAllServicePagination(int index);
        public int GetNumberTotalService();
        public int GetNumberService();
    }
}
