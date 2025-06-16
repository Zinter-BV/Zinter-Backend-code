using Microsoft.AspNetCore.Mvc;
using LogisticsSolution.Infrastructure.ExternalServices;
using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly MongoDbService<Delivery> _mongoService;

        public DeliveryController(MongoDbService<Delivery> mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetAll()
        {
            var deliveries = await _mongoService.GetAllAsync();
            return Ok(deliveries);
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Delivery delivery)
        {
            await _mongoService.InsertAsync(delivery);
            return Ok(new { success = true });
        }
    }
}
