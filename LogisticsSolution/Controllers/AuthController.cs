using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsSolution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAgent(AgentRegistrationDto request)
        {
            var result = await _auth.RegisterAgent(request);
            return Ok(result);
        }

    }
}
