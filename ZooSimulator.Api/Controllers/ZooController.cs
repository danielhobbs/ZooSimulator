using Microsoft.AspNetCore.Mvc;

namespace ZooSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooController : ControllerBase
    {
        //TODO: complete this - stubbed only for now

        private readonly Services.IZooService _zooService;

        public ZooController(Services.IZooService zooService)
        {
            _zooService = zooService;
        }

        [HttpGet(Name = "Zoo")]
        public IActionResult GetZoo()
        {
            var zoo = _zooService.GetZoo();
            return Ok(zoo);
        }

        [HttpPost(Name = "Feed")]
        public IActionResult FeedAnimals()
        {
            var zoo = _zooService.GetZoo();
            _zooService.FeedAnimals();   

            return Ok(zoo);
        }
    }
}
