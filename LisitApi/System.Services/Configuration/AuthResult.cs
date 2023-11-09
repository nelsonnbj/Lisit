using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.Configuration
{
    public class AuthResult: Result
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
