using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Utility;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetProvinces()
        {
            var result = await _province.GetAllProvinces();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetActiveRequests(PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, true);
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> GetRequestHistory(PaginationDto request)
        {
            var result = await _province.GetProvinceRequestsByAgent(request, false);
            return Ok(result);

        }
    }
}
