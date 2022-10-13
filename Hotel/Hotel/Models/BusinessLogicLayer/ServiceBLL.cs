using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.BusinessLogicLayer
{
    internal class ServiceBLL
    {
        HotelEntities context = new HotelEntities();
        public List<Service> GetAllServicesForRoom()
        {
            var list = context.GetAllServices();
            List<Service> services = new List<Service>();
            foreach (var service in list)
            {
                
            Service service1=new Service();
                service1.price = service.price;
               service1.name = service.name;
                services.Add(service1);
            }
            return services;
        }
    }
}
