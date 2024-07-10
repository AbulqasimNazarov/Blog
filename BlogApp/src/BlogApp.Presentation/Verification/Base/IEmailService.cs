namespace BlogApp.Presentation.Verification.Base
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);
    }
}