using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace System.WebApi.DTos
{
    public class validationImagen : ValidationAttribute
    {
        private readonly int HeightImageMegaBytes;

        public validationImagen(int HeightImageMegaBytes)
        {
            this.HeightImageMegaBytes = HeightImageMegaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;

            if (formFile.Length > HeightImageMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {HeightImageMegaBytes}mb");
            }

            return ValidationResult.Success;
        }

    }
}
