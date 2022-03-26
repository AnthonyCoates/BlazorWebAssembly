using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Service>> GetServices();
        Task<IEnumerable<Service>> Search(string name);

        Task<Service> GetService(int serviceID);
        Task<Service> AddService(Service service);
        Task<Service> UpdateService(Service service);
        Task DeleteService(int serviceID);
    }
}
