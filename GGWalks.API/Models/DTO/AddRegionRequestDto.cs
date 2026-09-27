using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GGWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MaxLength(4, ErrorMessage = "Code should be maximum of 4 characters")]
        [MinLength(2, ErrorMessage = "Code should be minimum of 2 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name should be maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
