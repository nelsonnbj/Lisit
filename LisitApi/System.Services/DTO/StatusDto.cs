using System.ComponentModel.DataAnnotations;

namespace System.Infrastructure.DTO
{
    public class StatusDto
    {
        public Guid Id { get; set; }
        [RegularExpression("^[A-ZÑÁÉÍÓÚ ]+$", ErrorMessage = "* .. solo letras mayúsculas")]
        public string Name { get; set; }
    }
}
