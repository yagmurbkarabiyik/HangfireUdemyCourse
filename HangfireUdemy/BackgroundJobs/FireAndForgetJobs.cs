using HangfireUdemy.Services;

namespace HangfireUdemy.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void EmailSendToUserJob(string userId, string message)
        {
            //enqueue tek seferlik işlemler için
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));
        }
    }
}
