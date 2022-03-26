using Microsoft.EntityFrameworkCore;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public class ServicesSoldRepository : IServicesSoldRepository
    {
        private readonly AppDBContext appDBContext;

        public ServicesSoldRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<ServiceSold> GetServiceSold(int appointmentID, int serviceID)
        {
            return await appDBContext.ServicesSold
                .FirstOrDefaultAsync(a => a.AppointmentID == appointmentID && a.ServiceID == serviceID);
        }

        public async Task<IEnumerable<ServiceSold>> GetServicesSold()
        {
            return await appDBContext.ServicesSold.ToListAsync();
        }

        public async Task<IEnumerable<ServiceSold>> GetServicesSoldByAppointment(int appointmentID)
        {
            IQueryable<ServiceSold> query = appDBContext.ServicesSold;

            return await query.Where(a => a.AppointmentID == appointmentID).ToListAsync();
        }

        public async Task<IEnumerable<ServiceSold>> GetServicesSoldByService(int serviceID)
        {
            IQueryable<ServiceSold> query = appDBContext.ServicesSold;

            return await query.Where(a => a.ServiceID == serviceID).ToListAsync();
        }
    }
}
