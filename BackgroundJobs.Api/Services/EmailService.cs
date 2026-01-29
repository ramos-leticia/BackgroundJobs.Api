
using Hangfire;

namespace BackgroundJobs.Api.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendWelcomeEmailAsync(string email, string name)
        {
            await Task.Delay(2000);
            Console.WriteLine($"[EMAIL] Enviando email de boas-vindas para {name} ({email})");
        }

        [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 10, 30, 60 })]
        public async Task SendWelcomeEmailWithErrorAsync(string email, string name)
        {
            await Task.Delay(1000);
            Console.WriteLine($"[EMAIL] Simulando falha no envio para {email}");
            throw new Exception("Erro simulado no envio de email");
        }
    }
}
