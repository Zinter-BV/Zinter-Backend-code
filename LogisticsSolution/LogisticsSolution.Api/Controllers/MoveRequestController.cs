using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuote(MoveRequestDto request)
        {
            var result = await _move.CreateRequest(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<MoveDetailsResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoveDetails(string code)
        {
            var result = await _move.GetDetailsByCode(code);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<List<AnalysedImageResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemsByImage(List<IFormFile> images)
        {
            var result = await _move.GetItemsByImage(images);
            return Ok(result);
        }
    }
}
