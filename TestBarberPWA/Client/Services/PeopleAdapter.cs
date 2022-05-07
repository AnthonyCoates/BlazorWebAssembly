using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Client.Services
{
    public class PeopleAdapter : DataAdaptor
    {
        private readonly IPeopleService peopleService;

        public PeopleAdapter(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        public async override Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            PeopleDataResult result =  await peopleService.GetPeople(dataManagerRequest.Skip, dataManagerRequest.Take);

            DataResult dataResult = new DataResult()
            {
                Result = result.People,
                Count = result.Count
            };

            return dataResult;
        }
    }
}
