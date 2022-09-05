using Microsoft.AspNetCore.Mvc;
using payCoreHW3.Context;
using payCoreHW3.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace payCoreHW3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly IMapperSession _session;
        //injection
        public VehicleController(IMapperSession session)
        {
            _session = session;
        }      
        
        // GET
        
        [HttpGet]
        public List<Vehicle> Get()
        {
            // all vehicles
            var result = _session.Vehicles.ToList();
            // return vehicle list
            return result;
        }
        // GET by Id

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            // finding vehicle using given id
            var result = _session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            // Check if the vehicle is exists or not.
            if (result == null) return BadRequest("Vehicle cannot be founded.");
            // return specific container
            return Ok(result);
        }
        // POST(Create)

        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            try
            {
                // BeginTransaction
                _session.BeginTransaction();
                // Save vehicle(comes from body)
                _session.Save(vehicle);
                //Commit operation
                _session.Commit();

            }
            catch (Exception exception)
            {
                // If something went wrong above then Rollback operations.
                _session.Rollback();
                throw;
            }
            finally
            {
                // At the end CloseTransaction, either operation success or not.
                _session.CloseTransaction();
            }
            // If create operation succeeded then return Ok and message;
            return Ok("Vehicle successfully created.");
        }

        // PUT(Update)
        [HttpPut]
        public ActionResult<Vehicle> Put([FromBody] Vehicle vehicleRequest)
        {
            //Finding vehicle using id.
            var vehicle = _session.Vehicles.Where(x => x.Id == vehicleRequest.Id).FirstOrDefault();
            // Check vehicle is exists or not.
            if (vehicle == null) return BadRequest("Vehicle does not exists.");
            // Change old VehicleName and old VehiclePlate to new VehicleName and new VehiclePlate.
            vehicle.VehicleName = vehicleRequest.VehicleName;
            vehicle.VehiclePlate = vehicleRequest.VehiclePlate;

            try
            {
                // Start Transaction
                _session.BeginTransaction();
                // Update our vehicle
                _session.Update(vehicle);
                // Commit
                _session.Commit();

            }
            catch (Exception e)
            {
                //Rollback if something went wrong.
                _session.Rollback();
                throw;
            }
            finally
            {
                // At the end CloseTransaction, either operation success or not.
                _session.CloseTransaction();
            }
            // if everything is fine then return Ok with message.
            return Ok("Vehicle is updated successfully.");
        }
        
        // DELETE

        [HttpDelete("{id}")]

        public IActionResult Delete(long id)
        {
            // Find vehicle using given id.
            var vehicle = _session.Vehicles.Where(x => x.Id == id).FirstOrDefault();
            //Check vehicle is exists or not.
            if (vehicle == null) return BadRequest("Vehicle can not found.");
            // Finding  containers belongs to our vehicle using containers vehicleId and vehicles id.
            var vehicleContainers = _session.Containers.Where(x => x.VehicleId == id).ToList();
            try
            {
                // Begin transaction
                _session.BeginTransaction();
                // Check vehicleContainers list empty or not.
                if (vehicleContainers.Count!=0)
                {
                    // If its not empty then delete each element using foreach
                    foreach (var container in vehicleContainers)
                    {
                        //Delete operation
                        _session.Delete(container);
                    }
                }
                // Then delete vehicle
                _session.Delete(vehicle);
                // Commit
                _session.Commit();

            }
            catch (Exception e)
            {
                //Rollback if something went wrong.
                _session.Rollback();
                throw;
            }
            finally
            {
                // At the end CloseTransaction, either operation success or not.
                _session.CloseTransaction();
            }
            // if everything is fine then return Ok with message.
            return Ok("Vehicle is deleted successfully.");
        }
    }
}

