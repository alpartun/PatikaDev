using System.Collections;
using Microsoft.AspNetCore.Mvc;
using payCoreHW3.Context;
using payCoreHW3.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace payCoreHW3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContainerController : ControllerBase
    {
        private readonly IMapperSession _session;
        //injection

        public ContainerController(IMapperSession session)
        {
            _session = session;
        }

        // GET
        [HttpGet]
        public List<Container> Get()
        {
            //All Containers
            var result = _session.Containers.ToList();
            // return Container list
            return result;
        }
        // GET Containers by VehicleId

        [HttpGet("{vehicleId}")]
        public IActionResult GetContainersByVehicleId(long vehicleId)
        {
            // get containers belongs to given vehicleId
            var containers = _session.Containers.Where(x => x.VehicleId == vehicleId).ToList();
            //check its empty or not
            if (containers.Count == 0) return Ok("Does not exists.");
            // return container(s)
            return Ok(containers);
        }


        // POST(Create)
        [HttpPost]
        public IActionResult Post([FromBody] Container container)
        {
            try
            {
                // Start transaction
                _session.BeginTransaction();
                // Save container
                _session.Save(container);
                // commit operation
                _session.Commit();
            }
            catch (Exception e)
            {
                // if something went wrong
                //rollback operation
                _session.Rollback();
                throw;
            }
            finally
            {
                // closing transaction 
                _session.CloseTransaction();
            }


            // if everything is fine returns our new created container
            return Ok("Container is created.");
        }

        // PUT(Update)

        [HttpPut]
        public IActionResult Put(Container container)
        {
            // getting our old container using container.Id which is coming from request
            var containerUpdate = _session.Containers.Where(x => x.Id == container.Id).FirstOrDefault();
            // check container exists or not.
            if (containerUpdate == null) return BadRequest("Container does not exists.");
            // Then create new container, changing values except Id and vehicleId those values comes from old one.
            containerUpdate.ContainerName = container.ContainerName;
            containerUpdate.Latitude = container.Latitude;
            containerUpdate.Longitude = container.Longitude;
            containerUpdate.VehicleId = container.VehicleId;
            try
            {
                // start transaction
                _session.BeginTransaction();
                // update our container
                _session.Update(containerUpdate);
                // commit
                _session.Commit();
            }

            catch (Exception ex)
            {
                // if something went wrong
                // rollback our operation
                _session.Rollback();
                throw;
            }
            finally
            {
                // close transaction
                _session.CloseTransaction();
            }

            // if everything is oki then send Ok with message
            return Ok("Container is updated.");
        }

        // DELETE

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            // find container using given id
            var containerDelete = _session.Containers.Where(x => x.Id == id).FirstOrDefault();
            // check container exists or not
            if (containerDelete == null) return BadRequest("Container does not exists.");

            try
            {
                // start transaction
                _session.BeginTransaction();
                // delete our container
                _session.Delete(containerDelete);
                // commit
                _session.Commit();
            }
            catch (Exception e)
            {
                // if something went wrong
                // rollback our operation
                _session.Rollback();
                throw;
            }
            finally
            {
                // close transaction
                _session.CloseTransaction();
            }

            // if everything is fine then return Ok with message
            return Ok("Container is deleted.");
        }

        // Container Cluster
        [HttpPost("Cluster")]
        public IActionResult Cluster([FromQuery] long vehicleId, int numberOfClusters)
        {


            // Find containers with same VehicleId
            var containerList = _session.Containers.Where(x => x.VehicleId == vehicleId).ToList();
            // check given numberOfClusters 0 or less.
            if (numberOfClusters < 0) return BadRequest("numberOfClusters can not be 0 or less.");
            // check given numberOfClusters bigger than container size.
            if (numberOfClusters > containerList.Count)
                return BadRequest("numberOfClusters can not be bigger than container size.");
            // Divide clusters with given numberOfClusters value using Select and GroupBy, i take module(remainder) of indexes because ->

            // Let say we have 8 containers then we have 0-7 indexes and we want to separate 2 groups.

            // index=0 % 2 = 0 (first group)
            // index=1 % 2 = 1 (second group)
            // index=2 % 2 = 0 (first group)
            // index=3 % 2 = 1 (second group)
            // index=4 % 2 = 0 (first group)
            // index=5 % 2 = 1 (second group)
            // index=6 % 2 = 0 (first group)
            // index=7 % 2 = 1 (second group)

            // Its basic math and suitable for this assignment actually.

            var list = containerList.Select(
                    (container, index) => new
                    {
                        ClusterIndex = index % numberOfClusters /*Module(remainder)*/, Item = container
                    })
                .GroupBy(i => i.ClusterIndex, g => g.Item).ToList();
            return Ok(list);

        }

    }
}
