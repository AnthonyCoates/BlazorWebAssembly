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

        public async Task<IEnumerable<Service>> Search(string name)
        {
            IQueryable<Service> query = appDBContext.Services;

            if (!string.IsNullOrEmpty(name))
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

        public async Task<Service> AddService(Service service)
        {
            var result = await appDBContext.Services.AddAsync(service);
            await appDBContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Service> UpdateService(Service service)
        {
            var result = await appDBContext.Services.FirstOrDefaultAsync(s => service.ServiceID == service.ServiceID);

            if (result != null)
            {
                result.Name = service.Name;
                result.Description = service.Description;
                result.Price = service.Price;

                await appDBContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteService(int serviceID)
        {
            var result = await appDBContext.Services.FirstOrDefaultAsync(s => s.ServiceID == serviceID);

            if (result != null)
            {
                appDBContext.Services.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }
    }
}
