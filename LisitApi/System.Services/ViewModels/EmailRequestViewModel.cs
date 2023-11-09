using System.ComponentModel.DataAnnotations;

namespace System.Infrastructure.ViewModels
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
