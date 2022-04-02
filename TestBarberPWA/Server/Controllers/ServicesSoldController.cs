using Microsoft.AspNetCore.Mvc;
using TestBarberPWA.Server.Models;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesSoldController : ControllerBase
    {
        private readonly IServicesSoldRepository servicesSoldRepository;

        public ServicesSoldController(IServicesSoldRepository servicesSoldRepository)
        {
            this.servicesSoldRepository = servicesSoldRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetServicesSold()
        {
            try
            {
                return Ok(await servicesSoldRepository.GetServicesSold());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Services Sold data from the database.");
            }
        }

        [HttpGet("appointment/{id:int}")]
        public async Task<ActionResult> GetServicesSoldByAppointment(int id)
        {
            try
            {
                return Ok(await servicesSoldRepository.GetServicesSoldByAppointment(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Services Sold data from the database.");
            }
        }

        [HttpGet("service/{id:int}")]
        public async Task<ActionResult> GetServicesSoldByService(int id)
        {
            try
            {
                return Ok(await servicesSoldRepository.GetServicesSoldByService(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Services Sold data from the database.");
            }
        }

        [HttpGet("{appointmentID:int}/{serviceID:int}")]
        public async Task<ActionResult<ServiceSold>> GetServiceSold(int appointmentID, int serviceID)
        {
            try
            {
                var result = await servicesSoldRepository.GetServiceSold(appointmentID, serviceID);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Services Sold data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceSold>> CreateServiceSold(ServiceSold serviceSold)
        {
            try
            {
                if (serviceSold == null)
                {
                    return BadRequest();
                }

                var createdServiceSold = await servicesSoldRepository.AddServiceSold(serviceSold);

                return CreatedAtAction(nameof(GetServiceSold), new { appointmentID = createdServiceSold.AppointmentID, serviceID = createdServiceSold.ServiceID }, createdServiceSold);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new service sold in the database.");
            }
        }

        [HttpPut("{appointmentID:int}/{serviceID:int}")]
        public async Task<ActionResult<ServiceSold>> UpdateServiceSold(int appointmentID, int serviceID, ServiceSold serviceSold)
        {
            try
            {
                if (appointmentID != serviceSold.AppointmentID)
                {
                    return BadRequest("Appointment ID provided did not match the service sold's details.");
                }

                if (serviceID != serviceSold.ServiceID)
                {
                    return BadRequest("Service ID provided did not match the service sold's details.");
                }
                
                var serviceSoldToUpdate = await servicesSoldRepository.GetServiceSold(appointmentID, serviceID);

                if (serviceSoldToUpdate == null)
                {
                    return NotFound($"Service sold with Appointment ID {appointmentID} and Service ID {serviceID} not found");
                }

                return await servicesSoldRepository.UpdateServiceSold(serviceSold);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the service sold in the database.");
            }
        }

        [HttpDelete("{appointmentID:int}/{serviceID:int}")]
        public async Task<ActionResult> DeleteServiceSold(int appointmentID, int serviceID)
        {
            try
            {
                var serviceSoldToDelete = await servicesSoldRepository.GetServiceSold(appointmentID, serviceID);

                if (serviceSoldToDelete == null)
                {
                    return NotFound($"Servive sold with Appointment ID {appointmentID} and Service ID {serviceID} not found");
                }

                await servicesSoldRepository.DeleteServiceSold(appointmentID, serviceID);

                return Ok($"Service sold with Appointment ID {appointmentID} and Service ID {serviceID} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the service sold from the database.");
            }
        }
    }
}
