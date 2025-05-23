using LogisticsSolution.Application.Contract;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Domain.Entities;

namespace LogisticsSolution.Application.BusinessLogic
{
    public class MailingService : IMailing
    {
        private readonly ILogger<MailingService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public MailingService(ILogger<MailingService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<string>> AddEmail(string email)
        {
            try
            {
                if (!email.IsValidEmail())
                    return "Invalid Email Address".FailResponse<string>();

                if(await _unitOfWork.GetRepository<Mailing>().AnyAsync(x => x.Email.ToLower() == email.ToLower()))
                    return "Already added to Mailing list".SuccessfulResponse();

                var newEmail = new Mailing
                {
                    Email = email,
                };

                await _unitOfWork.GetRepository<Mailing>().AddAsync(newEmail);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("New email added to the mailing list {email}  ::: {date}", email, DateTime.UtcNow);

                return "Added to Mailing list".SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError("AddEmail :::: {ex}", ex);
                return "Unable to add email".FailResponse<string>();
            }
        }

        public async Task<ResponseModel<List<string>>> GetAllEmails()
        {
            try
            {
                var emails = new List<string>();

                var mailingList = await _unitOfWork.GetRepository<Mailing>().FindAllAsync();
                if(mailingList.Count < 1)
                    return emails.SuccessfulResponse();

                foreach(var email in mailingList)
                {
                    emails.Add(email.Email);
                }

                return emails.SuccessfulResponse();

            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllEmails :::: {ex}", ex);
                return "Unable to get emails".FailResponse<List<string>>();
            }
        }
    }
}
