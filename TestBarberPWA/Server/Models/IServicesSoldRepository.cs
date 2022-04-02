using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IServicesSoldRepository
    {
        Task<IEnumerable<ServiceSold>> GetServicesSold();
        Task<IEnumerable<ServiceSold>> GetServicesSoldByAppointment(int appointmentID);
        Task<IEnumerable<ServiceSold>> GetServicesSoldByService(int serviceID);

        Task<ServiceSold> GetServiceSold(int appointmentID, int serviceID);

        Task<ServiceSold> AddServiceSold(ServiceSold serviceSold);
        Task<ServiceSold> UpdateServiceSold(ServiceSold serviceSold);
        Task DeleteServiceSold(int appointmentID, int serviceID);
    }
}
