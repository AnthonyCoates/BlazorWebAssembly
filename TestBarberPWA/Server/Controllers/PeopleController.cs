using TestBarberPWA.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestBarberPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            this.peopleRepository = peopleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPeople()
        {
            //try
            //{
            //    return Ok(await peopleRepository.GetPeople());
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving People data from the database.");
            //}

            return Ok(await peopleRepository.GetPeople());
        }
    }
}
