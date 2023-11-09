using System.ComponentModel.DataAnnotations;

namespace System.WebApi.DTos.Account
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
