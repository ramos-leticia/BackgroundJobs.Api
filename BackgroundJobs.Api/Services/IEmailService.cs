namespace BackgroundJobs.Api.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email, string name);
        Task SendWelcomeEmailWithErrorAsync(string email, string name);

    }
}
