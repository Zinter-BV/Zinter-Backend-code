using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;

namespace LogisticsSolution.Application.Contract
{
    public interface IQuote
    {
        Task<ResponseModel<string>> CreateQuote(CreateQuoteDto request);
        Task<ResponseModel<QuoteSummaryResponseModel>> GetQuoteDetails(long id);
        Task<ResponseModel<List<QuoteSummaryResponseModel>>> GetQuotesByMoveCode(string code);
    }
}
