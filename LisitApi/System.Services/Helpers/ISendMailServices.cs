using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Helpers
{
    public interface ISendMailServices
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
