using Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(
            string to,
            string from,
            string subject,
            string body)
        {
            // This could be implemented by another developer, or even a separate team. This architecture
            // supports breaking down work very nicely
            throw new NotImplementedException();
        }
    }
}