using Microsoft.EntityFrameworkCore;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public class AddressRepository : IAddressesRepository
    {
        private readonly AppDBContext appDBContext;

        public AddressRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<IEnumerable<Address>> GetAddress(string postcode)
        {
            IQueryable<Address> query = appDBContext.Addresses;

            return await query.Where(a => a.PostCode.Contains(postcode)).ToListAsync();
        }

        public async Task<Address> GetAddress(int addressID)
        {
            return await appDBContext.Addresses
                .FirstOrDefaultAsync(a => a.AddressID == addressID);
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await appDBContext.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddresses(int personID)
        {
            IQueryable<Address> query = appDBContext.Addresses;

            return await query.Where(a => a.PersonID == personID).ToListAsync();
        }

        public async Task<IEnumerable<Address>> Search(string? address, string? postcode)
        {
            IQueryable<Address> query = appDBContext.Addresses;

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(a => a.LineOne.Contains(address) || a.LineTwo.Contains(address));
            }

            if (!string.IsNullOrEmpty(postcode))
            {
                query = query.Where(a => a.PostCode.Contains(postcode));
            }

            return await query.ToListAsync();
        }
    }
}
