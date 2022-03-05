using Microsoft.EntityFrameworkCore;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly AppDBContext appDBContext;

        public ServicesRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<IEnumerable<Service>> GetService(string name)
        {
            IQueryable<Service> query = appDBContext.Services;

            if (string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.Name.Contains(name) || s.Description.Contains(name));

                return await query.ToListAsync();
            }

            return null;
        }

        public async Task<Service> GetService(int serviceID)
        {
            return await appDBContext.Services.FirstOrDefaultAsync(s => s.ServiceID == serviceID);
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await appDBContext.Services.ToListAsync();
        }
    }
}
