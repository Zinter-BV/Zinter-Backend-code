using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; // ✅ Required for Task<>
using System; // Optional but useful
using System.Collections.Generic; // If you return or use List<T> in the future

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
        public async Task<IActionResult> GetVerificationCode(EmailVerificationDto request)
        {
            var result = await _agent.SendEmailVerificationCode(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyDetailsByKvkNumber(string kvkNumber)
        {
            var result = await _agent.GetCompanyDetailsByKvkNumber(kvkNumber);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyCode(string email, string code)
        {
            var result = await _agent.VerifyCode(email, code);
            return Ok(result);
        }
    }
}
