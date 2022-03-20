using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Address>> Search(string? address, string? postcode);
        Task<IEnumerable<Address>> GetAddresses();
        Task<IEnumerable<Address>> GetAddresses(int personID);
        Task<IEnumerable<Address>> GetAddress(string postcode);

        Task<Address> GetAddress(int addressID);
    }
}
