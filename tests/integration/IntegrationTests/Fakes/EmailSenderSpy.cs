using Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.Fakes
{
    public class EmailSenderSpy : IEmailSender
    {
        public IList<EmailParameters> RecordedEmails { get; set; }

        public EmailSenderSpy()
        {
            RecordedEmails = new List<EmailParameters>();
        }

        public void Reset()
        {
            RecordedEmails.Clear();
        }

        public Task SendEmailAsync(string to, string from, string subject, string body)
        {
            RecordedEmails.Add(new EmailParameters
            {
                To = to,
                From = from,
                Subject = subject,
                Body = body
            });

            return Task.CompletedTask;
        }
    }

    public class EmailParameters
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}