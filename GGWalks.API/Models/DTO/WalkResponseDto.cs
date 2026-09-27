using GGWalks.API.Models.Domain;

namespace GGWalks.API.Models.DTO.Walk
{
    public class WalkResponseDto
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
