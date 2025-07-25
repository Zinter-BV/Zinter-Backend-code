﻿using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Dtos.Request;
using LogisticsSolution.Application.Dtos.Response;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class QuoteService : IQuote
    {
        private readonly ILogger<QuoteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public QuoteService(ILogger<QuoteService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //create quote
        public async Task<ResponseModel<string>> CreateQuote(CreateQuoteDto request)
        {
            try
            {
                //modify

                var all = await _unitOfWork.GetRepository<MovingAgent>().FindAllAsync();
                int user = all.FirstOrDefault().Id;

                var moveRequest = await _unitOfWork.GetRepository<MoveRequest>().FindSingleWithRelatedEntitiesAsync(x => x.Id == request.MoveId && x.MoveTime < DateTime.UtcNow);

                if (moveRequest == null)
                    "Invalid move request".FailResponse<string>();
                var newQuote = new Quote
                {
                    MoveRequestId = request.MoveId,
                    MovingAgentId = user,
                    Amount = request.Amount,
                    AdditonalInformation = request.AdditonalInformation,
                    ProposedTime = request.ProposedTime,
                };

                await _unitOfWork.GetRepository<Quote>().AddAsync(newQuote);
                await _unitOfWork.SaveChangesAsync();

                //send alert email

                _logger.LogInformation("{user} created a quote for move:: {moveId}  ::: {time}", user, request.MoveId, DateTime.UtcNow);
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

                var quote = await _unitOfWork.GetRepository<Quote>().FindSingleWithRelatedEntitiesAsync(x => x.Id == id, x => x.MovingAgent);

                if (quote == null)
                    return "quote details not found".FailResponse<QuoteSummaryResponseModel>();

                var response = new QuoteSummaryResponseModel
                {
                    QuoteId = id,
                    Amount = quote.Amount,
                    AdditionalInformation = quote.AdditonalInformation,
                    CompanyEmail = quote.MovingAgent.Email,
                    CompanyName = quote.MovingAgent.CompanyName,
                    ProposedTime = quote.ProposedTime,
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
