using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Integration.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(
            string to,
            string from,
            string subject,
            string body)
        {
            // This could be implemented by another developer, or even a separate team. This architecture
            // supports breaking down work very nicely
            _logger.LogInformation("Simulating sending an email...");

            return Task.CompletedTask;
        }
    }
}