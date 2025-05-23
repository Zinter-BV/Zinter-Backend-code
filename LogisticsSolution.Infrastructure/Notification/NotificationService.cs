using LogisticsSolution.Application.Constant;
using LogisticsSolution.Application.Contract.Notification;
using LogisticsSolution.Application.Utility;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Net.Mail;
using System.Net;

namespace LogisticsSolution.Infrastructure.Notification
{
    public class NotificationService : INotification
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly ConcurrentDictionary<string, string> _userConnections;
        private readonly AppSettings _appSettings;
        public NotificationService(ILogger<NotificationService> logger, IOptions<AppSettings> appSettings, IHubContext<NotificationHub> hub)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _hub = hub;
            _userConnections = new ConcurrentDictionary<string, string>();
        }


        public async Task SendEmailAsync(EmailDto emailDto)
        {
            try
            {
                using (var client = new SmtpClient(_appSettings.SmtpServer, _appSettings.SmtpPort))
                {
                    client.Credentials = new NetworkCredential(_appSettings.SmtpUser, _appSettings.SmtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_appSettings.SmtpUser),
                        Subject = emailDto.Subject,
                        Body = emailDto.Body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(emailDto.To);

                    if (emailDto.Cc != null)
                    {
                        foreach (var cc in emailDto.Cc)
                        {
                            mailMessage.CC.Add(cc);
                        }
                    }

                    if (emailDto.Bcc != null)
                    {
                        foreach (var bcc in emailDto.Bcc)
                        {
                            mailMessage.Bcc.Add(bcc);
                        }
                    }

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation($"Email successfully sent :::: To ==> {emailDto.To}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
            }
        }

        public void AddSignalRConnection(string userId, string connectionId)
        {
            _userConnections[userId] = connectionId;

        }

        public void RemoveSignalRConnection(string userId)
        {
            _userConnections.TryRemove(userId, out _);
        }
    }
}
