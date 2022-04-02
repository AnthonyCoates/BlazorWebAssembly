using TestBarberPWA.Server.Models;
using Microsoft.AspNetCore.Mvc;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            this.servicesRepository = servicesRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetServices()
        {
            try
            {
                return Ok(await servicesRepository.GetServices());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Service data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            try
            {
                var result = await servicesRepository.GetService(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Service data from the database.");
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetService(string name)
        {
            try
            {
                var result = await servicesRepository.Search(name);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Service data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> AddService(Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest();
                }

                var addedService = await servicesRepository.AddService(service);

                return CreatedAtAction(nameof(GetService), new { id = addedService.ServiceID }, addedService);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new service in the database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Service>> UpdateService(int id, Service service)
        {
            try
            {
                if (id != service.ServiceID)
                {
                    return BadRequest("Service ID provided did not match the service's details.");
                }

                var personToUpdate = await servicesRepository.GetService(id);

                if (personToUpdate == null)
                {
                    return NotFound($"Service with ID {id} not found");
                }

                return await servicesRepository.UpdateService(service);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the service in the database.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                var personToDelete = await servicesRepository.GetService(id);

                if (personToDelete == null)
                {
                    return NotFound($"Service with ID {id} not found");
                }

                await servicesRepository.DeleteService(id);

                return Ok($"Service with ID {id} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the service from the database.");
            }
        }
    }
}
