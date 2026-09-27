using GGWalks.API.Models.Domain;

namespace GGWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery=null,
            string? sortBy = null, bool? isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?> DeleteWalkByIdAsync(Guid id);
        Task<Walk?> UpdateWalkByIdAsync(Guid id, Walk walk);
    }
}
