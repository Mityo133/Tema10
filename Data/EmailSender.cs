using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Themes.Data
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
