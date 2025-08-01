using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsSolution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvince _province;
        public ProvinceController(IProvince province)
        {
            _province = province;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<ProvinceResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProvinces()
        {
            var result = await _province.GetAllProvinces();
            return Ok(result);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(typeof(ResponseModel<Paged<PendingMoveRequestResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveRequests(PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, true, null);
            return Ok(result);

        }
        [HttpGet, Authorize]
        [ProducesResponseType(typeof(ResponseModel<Paged<PendingMoveRequestResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRequestHistory([FromQuery] MoveStatusEnum? moveStatus, [FromQuery] PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, false, moveStatus);
            return Ok(result);

        }
    }
}
