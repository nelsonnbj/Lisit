
using System.ComponentModel.DataAnnotations;

namespace System.Api.DTos.Account
{
    public class VerifyIdentityDto
    {

        [Display(Name = "AnnouncementId")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int AnnouncementId { get; set; }

        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Cedula { get; set; }
    }
} 