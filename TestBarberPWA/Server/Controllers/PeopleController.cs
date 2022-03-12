using TestBarberPWA.Server.Models;
using Microsoft.AspNetCore.Mvc;
using TestBarberPWA.Shared;

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
            try
            {
                return Ok(await peopleRepository.GetPeople());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving People data from the database.");
            }
        }

        [HttpGet("{ec:bool}")]
        public async Task<ActionResult> GetEmployeeOrCustomers(bool ec)
        {
            try
            {
                return Ok(await peopleRepository.GetEmployeesOrCustomers(ec));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving People data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            try
            {
                var result = await peopleRepository.GetPerson(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving People data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest();
                }

                var per = await peopleRepository.GetPersonByEmail(person.Email);

                if (per != null)
                {
                    ModelState.AddModelError("Email", "This email is already in use.");

                    return BadRequest(ModelState);
                }

                var createdPerson = await peopleRepository.AddPerson(person);

                return CreatedAtAction(nameof(GetPerson), new {id = createdPerson.PersonID}, createdPerson);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new person in the database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, Person person)
        {
            try
            {
                if (id != person.PersonID)
                {
                    return BadRequest("Person ID provided did not match the person's details.");
                }

                var personToUpdate = await peopleRepository.GetPerson(id);

                if (personToUpdate == null)
                {
                    return NotFound($"Person with ID {id} not found");
                }

                return await peopleRepository.UpdatePerson(person);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the person in the database.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                var personToDelete = await peopleRepository.GetPerson(id);

                if (personToDelete == null)
                {
                    return NotFound($"Person with ID {id} not found");
                }

                await peopleRepository.DeletePerson(id);

                return Ok($"Person with ID {id} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the person from the database.");
            }
        }
    }
}
