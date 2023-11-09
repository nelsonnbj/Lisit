using System.Infrastructure.Configuration;

namespace System.Infrastructure.Helpers
{
    public interface IMailServices
    {
        Result SendMail(string to, string subject, string body);
    }
}
