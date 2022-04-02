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

        public async Task<ServiceSold> AddServiceSold(ServiceSold serviceSold)
        {
            var result = await appDBContext.ServicesSold.AddAsync(serviceSold);
            await appDBContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<ServiceSold> UpdateServiceSold(ServiceSold serviceSold)
        {
            var result = await appDBContext.ServicesSold.FirstOrDefaultAsync(s => serviceSold.AppointmentID == serviceSold.AppointmentID && serviceSold.ServiceID == serviceSold.ServiceID);

            if (result != null)
            {
                result.AppointmentID = serviceSold.AppointmentID;
                result.ServiceID = serviceSold.ServiceID;
                result.Quantity = serviceSold.Quantity;
                result.SubTotal = serviceSold.SubTotal;

                await appDBContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteServiceSold(int appointmentID, int serviceID)
        {
            var result = await appDBContext.ServicesSold.FirstOrDefaultAsync(s => s.AppointmentID == appointmentID && s.ServiceID == serviceID);

            if (result != null)
            {
                appDBContext.ServicesSold.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }
    }
}
