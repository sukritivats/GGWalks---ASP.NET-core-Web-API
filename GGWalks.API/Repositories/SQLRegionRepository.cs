using GGWalks.API.Data;
using GGWalks.API.Models.Domain;
using GGWalks.API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GGWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly GGWalksDBContext dBContext;

        public SQLRegionRepository(GGWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dBContext.Regions.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionByIdAsync(Guid id)
        {
           var regionDomain =  await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain != null)
            {
                dBContext.Regions.Remove(regionDomain);
                await dBContext.SaveChangesAsync();
                return regionDomain;
            }
            return null;
        }

        public async Task<List<Region>> GetAllRegionAsync()
        {
            return await dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateRegionByIdAsync(Guid id, Region region)
        {
            var existingRegionDomain = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegionDomain != null)
            {
                //update 
                existingRegionDomain.Code = region.Code;
                existingRegionDomain.Name = region.Name;
                existingRegionDomain.RegionImageUrl = region.RegionImageUrl;
                //save changes to dB
                await dBContext.SaveChangesAsync();
                return existingRegionDomain;
            }
            return null;
        }
    }
}
