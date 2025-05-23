using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

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

        [HttpPost]
        public async Task<IActionResult> CreateQuote(CreateQuoteDto request)
        {
            var result = await _quote.CreateQuote(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuoteDetails(long id)
        {
            var result = await _quote.GetQuoteDetails(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuotes(string code)
        {
            var result = await _quote.GetQuotesByMoveCode(code);
            return Ok(result);
        }
    }
}
