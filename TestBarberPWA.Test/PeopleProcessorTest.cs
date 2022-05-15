using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestBarberPWA.Server.Models;
using TestBarberPWA.Shared;
using Xunit;
using Shouldly;

namespace TestBarberPWA.Test
{
    public class PeopleProcessorTest
    {
        [Fact]
        public async void Should_Return_List_Of_All_People()
        {
            //Arrange - create representations of data to be testing against
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();

            var appDBContext = new AppDBContext(optionsBuilder.Options);

            var peopleRepository = new PeopleRepository(appDBContext);

            //Act - simulate a call to the method that should be called when enacting action
            var result = await peopleRepository.GetPeople();

            //Assert - what should the outcome be
            result.ShouldNotBeNull();

            
            
            appDBContext.Dispose(); //This is to dispose of db context without a using bracket
        }

        [Fact]
        public async void Should_Return_Person_Searched_By_Email()
        {
            //Arrange - create representations of data to be testing against
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();

            var appDBContext = new AppDBContext(optionsBuilder.Options);

            var peopleRepository = new PeopleRepository(appDBContext);

            string email = "a.aaronson@aaronmail.com";

            //Act - simulate a call to the method that should be called when enacting action
            var result = await peopleRepository.GetPersonByEmail(email);

            //Assert - what should the outcome be
            result.ShouldNotBeNull();
            result.Email.ShouldBe(email);


            //This is to dispose of db context without a using bracket
            appDBContext.Dispose();
        }
    }
}