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
    public class AgentController : ControllerBase
    {
        private readonly IAgent _agent;
        public AgentController(IAgent agent)
        {
            _agent = agent;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVerificationCode(EmailVerificationDto request)
        {
            var result = await _agent.SendEmailVerificationCode(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<KvkCompanyDetailsResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyDetailsByKvkNumber(string kvkNumber)
        {
            var result = await _agent.GetCompanyDetailsByKvkNumber(kvkNumber);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyCode(string email, string code)
        {
            var result = await _agent.VerifyCode(email, code);
            return Ok(result);
        }
    }
}
