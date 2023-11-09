using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Helpers
{
    public class SendMailHelpers : ISendMailServices
    {
        private string _key;
        private string _sender;

        public SendMailHelpers(IConfiguration configuration)
        {
            _key = configuration["Email:SENDGRID_KEY"];
            _sender = configuration["Email:SENDER_EMAIL"];
        }
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            var client = new SendGridClient(_key);
            var from = new EmailAddress(_sender, "Mescyt");
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
