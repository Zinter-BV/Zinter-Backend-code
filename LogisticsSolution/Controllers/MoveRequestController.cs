using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogisticsSolution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoveRequestController : ControllerBase
    {
        private readonly IMove _move;

        public MoveRequestController(IMove move)
        {
            _move = move;
        }

        [HttpPost]
        public async Task<IActionResult> GetQuote([FromBody] MoveRequestDto request)
        {
            var result = await _move.CreateRequest(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMoveDetails([FromQuery] string code)
        {
            var result = await _move.GetDetailsByCode(code);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemsByImage([FromForm] List<IFormFile> images)
        {
            var result = await _move.GetItemsByImage(images);
            return Ok(result);
        }
    }
}
