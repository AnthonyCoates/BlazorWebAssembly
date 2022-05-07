using System.Net.Http.Json;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Client.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly HttpClient httpClient;

        public PeopleService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Person> AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task DeletePerson(int personID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetEmployeesOrCustomers(bool isEmployee)
        {
            throw new NotImplementedException();
        }

        public async Task<PeopleDataResult> GetPeople(int skip, int take)
        {
            return await httpClient.GetFromJsonAsync<PeopleDataResult>($"/api/people?skip={skip}&take={take}");
        }

        public Task<Person> GetPerson(int personID)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
