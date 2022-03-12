using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> Search(string name, Gender? gender);
        Task<IEnumerable<Person>> GetPeople();
        Task<IEnumerable<Person>> GetEmployeesOrCustomers(bool isEmployee);

        Task<Person> GetPerson(int personID);
        Task<Person> GetPersonByEmail(string email);
        Task<Person> AddPerson(Person person);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(int personID);
    }
}
