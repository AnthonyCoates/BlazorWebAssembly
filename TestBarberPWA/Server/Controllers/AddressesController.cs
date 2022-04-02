using Microsoft.AspNetCore.Mvc;
using TestBarberPWA.Server.Models;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesRepository addressesRepository;

        public AddressesController(IAddressesRepository addressesRepository)
        {
            this.addressesRepository = addressesRepository;
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Address>>> Search(string? address, string? postcode)
        {
            try
            {
                var result = await addressesRepository.Search(address, postcode);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Address data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAddresses()
        {
            try
            {
                return Ok(await addressesRepository.GetAddresses());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Address data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var result = await addressesRepository.GetAddress(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Address data from the database.");
            }
        }

        [HttpGet("{pc}")]
        public async Task<ActionResult> GetAddress(string pc)
        {
            try
            {
                return Ok(await addressesRepository.GetAddress(pc));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving Address data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest();
                }

                var createdAddress = await addressesRepository.AddAddress(address);

                return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.AddressID }, createdAddress);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating a new address in the database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Address>> UpdateAddress(int id, Address address)
        {
            try
            {
                if (id != address.AddressID)
                {
                    return BadRequest("Person ID provided did not match the address' details.");
                }

                var addressToUpdate = await addressesRepository.GetAddress(id);

                if (addressToUpdate == null)
                {
                    return NotFound($"Address with ID {id} not found");
                }

                return await addressesRepository.UpdateAddress(address);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the address in the database.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            try
            {
                var addressToDelete = await addressesRepository.GetAddress(id);

                if (addressToDelete == null)
                {
                    return NotFound($"Address with ID {id} not found");
                }

                await addressesRepository.DeleteAddress(id);

                return Ok($"Address with ID {id} successfully deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the address from the database.");
            }
        }
    }
}
