using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DeliverNET
{
    public class MailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO Implement email API ("SendGrid")
            return Task.CompletedTask;
        }
    }
}