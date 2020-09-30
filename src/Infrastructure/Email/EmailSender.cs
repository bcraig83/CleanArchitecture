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
            // TODO: implement this.
            throw new NotImplementedException();
        }
    }
}