using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.DataServiceLayer.Contracts;
using Account.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace Account.DataServiceLayer.Handlers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfigurationDTO _emailConfig;

        public EmailSender(EmailConfigurationDTO emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(MessageDTO message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public void SendEmailString(string message)
        {
            throw new NotImplementedException();
        }

        private MimeMessage CreateEmailMessage(MessageDTO message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From,_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH");
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    //log an error message or throw an exception or both.
                    throw ex;
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


        //public EmailSender(ILogger<EmailSender> logger)
        //{
        //    this.logger = logger;
        //}

        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    this.logger.LogInformation($"{message}");
        //    return Task.CompletedTask;
//        }
//    }
//}
