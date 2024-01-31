namespace HangfireUdemy.Services
{
    public interface IEmailSender
    {
        Task Sender(string userId, string message);
    }
}
