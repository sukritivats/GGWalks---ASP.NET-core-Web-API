using System.ComponentModel.DataAnnotations;

namespace GGWalks.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Filename { get; set; }

        public string? FileDescription { get; set; }
    }
}
