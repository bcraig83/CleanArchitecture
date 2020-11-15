using Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Fakes
{
    public class EmailSenderStub : IEmailSender
    {
        public IList<EmailDetails> _recordedEmails { get; private set; } = new List<EmailDetails>();

        public Task SendEmailAsync(string to, string from, string subject, string body)
        {
            _recordedEmails.Add(new EmailDetails(to, from, subject, body));
            return Task.CompletedTask;
        }

        public void Clear()
        {
            _recordedEmails.Clear();
        }
    }

    public class EmailDetails
    {
        public EmailDetails(string to, string from, string subject, string body)
        {
            To = to;
            From = from;
            Subject = subject;
            Body = body;
        }

        public string To { get; private set; }
        public string From { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}