using Microsoft.Extensions.Logging;

namespace CrudChallenge.Notifications.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public void SendReceiptEmail(string receipt)
        {
            throw new System.NotImplementedException();
        }

        public void SendReportReadyEmail(string report)
        {
            _logger.LogInformation("Report email has been sent: {0}", report);
        }
    }
}
