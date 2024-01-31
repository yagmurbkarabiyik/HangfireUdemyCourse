using SendGrid;
using SendGrid.Helpers.Mail;

namespace HangfireUdemy.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Sender(string userId, string message)
        {
            static async Task Execute()
            {
                var apiKey = Environment.GetEnvironmentVariable("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");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("ygmr44@outlook.com", "Example User");
                var subject = "My hangfire project info mail";
                var to = new EmailAddress("ygmr44@outlook.com", "Example User");
                var htmlContent = "<strong>Test test test</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}