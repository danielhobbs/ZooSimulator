using Microsoft.AspNetCore.Mvc;

namespace ZooSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZooController : ControllerBase
    {
        // TODO Error handling and logging

        private readonly Services.IZooService _zooService;

        public ZooController(Services.IZooService zooService)
        {
            _zooService = zooService;
        }

        [HttpGet]
        public IActionResult GetZoo()
        {
            return Ok(_zooService.GetZoo());
        }

        [HttpPost("feed")]
        public IActionResult FeedAnimals()
        {
            _zooService.FeedAnimals();

            return Ok();
        }


        [HttpPost("interval")]
        public IActionResult TriggerInterval()
        {
            _zooService.ProcessInterval();

            return Ok();
        }
    }
}
