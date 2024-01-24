namespace CrudChallenge.Notifications.Services
{
    public interface IEmailService
    {
        void SendReportReadyEmail(string report);

        void SendReceiptEmail(string receipt);
    }
}