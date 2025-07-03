using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace LogisticsSolution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMailing _mailing;
        public MailingController(IMailing mailing)
        {
            _mailing = mailing;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(string email)
        {
            var result = await _mailing.AddEmail(email);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mailing.GetAllEmails();
            return Ok(result);
        }
    }
}
