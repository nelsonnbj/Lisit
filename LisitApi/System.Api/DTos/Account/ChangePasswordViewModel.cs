using System.ComponentModel.DataAnnotations;

namespace System.WebApi.DTos.Account
{
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
