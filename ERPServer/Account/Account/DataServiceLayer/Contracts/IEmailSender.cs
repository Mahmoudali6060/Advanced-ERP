using Account.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Account.DataServiceLayer.Contracts
{
    public interface IEmailSender
    {
      //  Task SendEmailAsync(string email, string subject, string message);
        void SendEmail(MessageDTO message);
        void SendEmailString(string message);
    }
}
