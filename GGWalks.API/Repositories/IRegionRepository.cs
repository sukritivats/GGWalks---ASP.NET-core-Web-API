using GGWalks.API.Models.Domain;

namespace GGWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionByIdAsync(Guid id, Region region);
        Task<Region?> DeleteRegionByIdAsync(Guid id); 
    }
}
