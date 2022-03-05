using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Service>> GetServices();
        Task<IEnumerable<Service>> GetService(string name);

        Task<Service> GetService(int serviceID);
    }
}
