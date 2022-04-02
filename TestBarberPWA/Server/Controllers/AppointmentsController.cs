using Microsoft.AspNetCore.Mvc;
using TestBarberPWA.Server.Models;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository appointmentsRepository;

        public AppointmentsController(IAppointmentsRepository appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> Search(string? note, DateTime? dateTime)
        {
            try
            {
                var result = await appointmentsRepository.Search(note, dateTime);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAppointments()
        {
            try
            {
                return Ok(await appointmentsRepository.GetAppointments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database.");
            }
        }

        [HttpGet("{dateTime:DateTime}")]
        public async Task<ActionResult> GetAppointments(DateTime dateTime)
        {
            try
            {
                return Ok(await appointmentsRepository.GetAppointments(dateTime));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            try
            {
                var result = await appointmentsRepository.GetAppointment(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database.");
            }
        }

        [HttpGet("customer/{id:int}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetCustomerAppointments(int id)
        {
            try
            {
                var result = await appointmentsRepository.GetCustomerAppointments(id);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database");
            }
        }

        [HttpGet("employee/{id:int}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetEmployeeAppointments(int id)
        {
            try
            {
                var result = await appointmentsRepository.GetEmployeeAppointments(id);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Appointments data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreatePerson(Appointment appointment)
        {
            try
            {
                if (appointment == null)
                {
                    return BadRequest();
                }

                var createdAppointment = await appointmentsRepository.AddAppointment(appointment);

                return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.AppointmentID }, createdAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new appointment in the database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Appointment>> UpdateAppointment(int id, Appointment appointment)
        {
            try
            {
                if (id != appointment.AppointmentID)
                {
                    return BadRequest("Person ID provided did not match the person's details.");
                }

                var appointmentToUpdate = await appointmentsRepository.GetAppointment(id);

                if (appointmentToUpdate == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                return await appointmentsRepository.UpdateAppointment(appointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the appointment in the database.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            try
            {
                var appointmentToDelete = await appointmentsRepository.GetAppointment(id);

                if (appointmentToDelete == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                await appointmentsRepository.DeleteAppointment(id);

                return Ok($"Appointment with ID {id} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the appointment from the database.");
            }
        }
    }
}
