using GGWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace GGWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
