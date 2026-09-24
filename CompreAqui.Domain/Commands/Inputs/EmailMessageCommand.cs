using CompreAqui.Domain.Contracts;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace CompreAqui.Domain.Commands.Inputs
{
    public class EmailMessageCommand :ICommand
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessageCommand(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }
    }
}
