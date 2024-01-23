namespace CrudChallenge.Notifications
{
    public interface IEmailService
    {
        void SendReportReadyEmail(string report);

        void SendReceiptEmail(string receipt);
    }
}