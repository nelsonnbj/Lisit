using System.ComponentModel.DataAnnotations;

namespace System.WebApi.DTos
{
    public class UserInfoViewModel
    {
     

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
