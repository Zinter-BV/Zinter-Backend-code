using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsSolution.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuote _quote;
        public QuoteController(IQuote quote)
        {
            _quote = quote;
        }

        [HttpPost, Authorize]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateQuote(CreateQuoteDto request)
        {
            var result = await _quote.CreateQuote(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<QuoteSummaryResponseModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuoteDetails(long id)
        {
            var result = await _quote.GetQuoteDetails(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<QuoteSummaryResponseModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllQuotes(string code)
        {
            var result = await _quote.GetQuotesByMoveCode(code);
            return Ok(result);
        }
    }
}
