using System.ComponentModel.DataAnnotations;

namespace System.WebApi.DTos.Account
{
    public class EmailRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
