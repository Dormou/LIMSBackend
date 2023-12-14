namespace AccessControl.Services;

public interface IEmailService
{
    public Task<bool> SendEmailAsync(string nick, string email, string subject, string message);
}
