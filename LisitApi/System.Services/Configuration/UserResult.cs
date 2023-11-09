using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.Configuration
{
    public class UserResult : Result
    {
        public Users Applicant { get; set; }
        public string Token { get; set; }
    }
}
