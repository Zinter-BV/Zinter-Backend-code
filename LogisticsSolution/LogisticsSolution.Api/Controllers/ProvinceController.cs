using Azure;
using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<Paged<PendingMoveRequestResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveRequests(PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, true);
            return Ok(result);

        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<Paged<PendingMoveRequestResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRequestHistory([FromQuery]PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, false);
            return Ok(result);

        }
    }
}
