using LogisticsSolution.Application.Utility;

namespace LogisticsSolution.Application.Contract.Notification
{
    public interface INotification
    {
        Task SendEmailAsync(EmailDto emailDto);
        void AddSignalRConnection(string userId, string connectionId);
        void RemoveSignalRConnection(string staffId);

    }
}
