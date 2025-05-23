namespace LogisticsSolution.Application.Contract
{
    public interface IMailing
    {
        Task<ResponseModel<string>> AddEmail(string email);
        Task<ResponseModel<List<string>>> GetAllEmails();
    }
}
