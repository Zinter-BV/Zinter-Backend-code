using Microsoft.AspNetCore.Mvc;
using LogisticsSolution.Domain.Entities;
// Update the namespace below to the correct one where AppDbContext is defined, for example:
using LogisticsSolution.Persistence;


namespace LogisticsSolution.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeliveryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDeliveries()
        {
            var deliveries = _context.Deliveries.ToList();
            return Ok(deliveries);
        }
    }
}
