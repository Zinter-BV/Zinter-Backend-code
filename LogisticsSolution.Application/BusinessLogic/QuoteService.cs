using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class QuoteService : IQuote
    {
        private readonly ILogger<QuoteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMove _move;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuoteService(ILogger<QuoteService> logger, IUnitOfWork unitOfWork, IMove move, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _move = move;
            _httpContextAccessor = httpContextAccessor;
        }

        //create quote
        public async Task<ResponseModel<string>> CreateQuote(CreateQuoteDto request)
        {
            try
            {
                HttpContextContent? jwtClaims = _httpContextAccessor.GetHttpContextValues();

                if (jwtClaims == null || jwtClaims.role == Domain.Enums.RoleEnum.MovingAgent)
                {
                    return null;
                }


                int userId = jwtClaims.userId;

                var moveRequest = await _unitOfWork.GetRepository<MoveRequest>().FindSingleWithRelatedEntitiesAsync(x => x.Id == request.MoveId && x.MoveTime < DateTime.UtcNow);

                if (moveRequest == null)
                    "Invalid move request".FailResponse<string>();
                var newQuote = new Quote
                {
                    MoveRequestId = request.MoveId,
                    MovingAgentId = userId,
                    Amount = request.Amount,
                    AdditonalInformation = request.AdditonalInformation,
                    ProposedTime = request.ProposedTime,
                };

                await _unitOfWork.GetRepository<Quote>().AddAsync(newQuote);
                await _unitOfWork.SaveChangesAsync();

                //send alert email

                _logger.LogInformation("{user} created a quote for move:: {moveId}  ::: {time}", userId, request.MoveId, DateTime.UtcNow);
                return "Quote sent to customer".SuccessfulResponse();


            }
            catch (Exception ex)
            {
                _logger.LogError("CreateQuote :::{ex}", ex);
                return "Unable to create Quote".FailResponse<string>();

            }
        }
        //get quote details 
        public async Task<ResponseModel<QuoteSummaryResponseModel>> GetQuoteDetails(long id)
        {
            try
            {

                var quote = await _unitOfWork.GetRepository<Quote>().FindSingleWithRelatedEntitiesAsync(x => x.Id == id, x => x.MovingAgent, x => x.MoveRequest);

                if (quote == null)
                    return "quote details not found".FailResponse<QuoteSummaryResponseModel>();

                var quoteDetails = await _move.GetDetailsByCode(quote.MoveRequest.MoveCode);

                var response = new QuoteSummaryResponseModel
                {
                    QuoteId = id,
                    Amount = quote.Amount,
                    AdditionalInformation = quote.AdditonalInformation,
                    CompanyEmail = quote.MovingAgent.Email,
                    CompanyName = quote.MovingAgent.CompanyName,
                    ProposedTime = quote.ProposedTime,
                    Image = quote.MovingAgent.Image,
                    MoveDetails = !quoteDetails.ResponseStatus ? new MoveDetailsResponseModel() : quoteDetails.Result
                };

                return response.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError("GetQuoteDetails :::: {ex}", ex);
                return "unable to retrieve details".FailResponse<QuoteSummaryResponseModel>();
            }
        }
        //get quote by moveCode
        public async Task<ResponseModel<List<QuoteSummaryResponseModel>>> GetQuotesByMoveCode(string code)
        {
            try
            {
                var quotes = new List<QuoteSummaryResponseModel>();

                var moveRequest = await _unitOfWork.GetRepository<MoveRequest>().FindSingleWithRelatedEntitiesAsync(x => x.MoveCode == code, x => x.Quotes);

                if (moveRequest is null)
                    return "move quotes not found".FailResponse<List<QuoteSummaryResponseModel>>();

                foreach (var quote in moveRequest.Quotes)
                {
                    var quoteDetails = await GetQuoteDetails(quote.Id);
                    if (quoteDetails.ResponseStatus)
                        quotes.Add(quoteDetails.Result);
                    continue;
                }

                return quotes.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError("GetQuotesByMoveCode ::: {ex}", ex);
                return "Unable to retrieve quotes".FailResponse<List<QuoteSummaryResponseModel>>();

            }
        }
    }
}
