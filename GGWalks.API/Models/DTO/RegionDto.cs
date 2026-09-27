namespace GGWalks.API.Models.DTO
{
    public class RegionDto
    { 
        // sukriti - contains all or some properties from region domain model which we want to expose to client
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
