
using CompreAqui.Domain.Commands.Inputs;
using CompreAqui.Domain.Contracts;
using CompreAqui.Domain.Models.Settings;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Security.Authentication;
using MailKit.Security;

namespace CompreAqui.Domain.Tools
{
    public class EmailSenderTool : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSenderTool(EmailSettings emailsettings)
        {
            _emailSettings = emailsettings;
        }
     
        public void SendEmail(EmailMessageCommand message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessageCommand message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            emailMessage.Body = new TextPart("html") { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.CheckCertificateRevocation = false;
                    client.SslProtocols = SslProtocols.Default;
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.Auto);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.AuthenticationMechanisms.Remove("PLAIN");
                    client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                    client.Send(mailMessage);
                }
                catch(Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }       
    }
}