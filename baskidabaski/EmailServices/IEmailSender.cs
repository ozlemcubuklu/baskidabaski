namespace baskidabaski.EmailServices
{
    public interface IEmailSender
    {
        bool SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
