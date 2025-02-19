using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static HRM_Management.Infrastructure.Helpers.HelpersMethods;
namespace HRM_Management.Infrastructure.Services
{
    public class SendingMailService : IMessageService
    {
        private readonly EmailConfiguration _options;
        private readonly ILogger<SendingMailService> _logger;
        public SendingMailService(IOptions<EmailConfiguration> options, ILogger<SendingMailService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendMessageAsync( string[] receivers, params SendingMessageDto[] messages)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_options.SmtpServer, _options.Port, true);
                    client.Authenticate(_options.From, _options.Password);
                    foreach (var message in messages)
                    {
                        var mimeMessage = CreateMimeMessage(message, _options, receivers);
                        await client.SendAsync(mimeMessage);
                    }
                }
                catch (MailKit.Security.AuthenticationException ex)
                {
                    _logger.LogError(ex, "Authentication failed. Check email credentials.");
                    throw new InvalidOperationException("Failed to authenticate with the SMTP server.", ex);
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    _logger.LogError(ex, "Network error while trying to send email.");
                    throw new InvalidOperationException("Failed to connect to the SMTP server.", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}
