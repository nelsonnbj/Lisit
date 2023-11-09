using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.DTO
{
    public class VerifyIdentityDTO
    {
        [Display(Name = "AnnouncementId")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid AnnouncementId { get; set; }

        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Cedula { get; set; }
    }
}
