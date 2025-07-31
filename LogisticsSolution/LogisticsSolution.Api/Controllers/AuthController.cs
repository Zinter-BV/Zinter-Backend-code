using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
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
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAgent(AgentRegistrationDto request)
        {
            var result = await _auth.RegisterAgent(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<LoginResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser(LoginDto request)
        {
            var result = await _auth.LoginUser(request);
            return Ok(result);
        }

    }
}
