using GGWalks.API.Data;
using GGWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GGWalks.API.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly GGWalksDBContext dBContext;

        public SqlWalkRepository(GGWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dBContext.AddAsync(walk);
            await dBContext.SaveChangesAsync();
            // Explicitly load the related properties from the database 
            // so they are available for mapping in your controller
            await dBContext.Entry(walk).Reference(w => w.Difficulty).LoadAsync();
            await dBContext.Entry(walk).Reference(w => w.Region).LoadAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkByIdAsync(Guid id)
        {
            var walkDomain = await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if(walkDomain!= null)
            {
                dBContext.Walks.Remove(walkDomain);
                await dBContext.SaveChangesAsync();
                return walkDomain;
            }
            return null;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool? isAscending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            //return await dBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

            var walks = dBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery ));
                }
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = (isAscending ?? true) ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x=> x.Name);
                }
            }

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalkByIdAsync(Guid id, Walk walk)
        {
            var walkDomain = await dBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomain != null)
            {
                //update data
                walkDomain.Name = walk.Name;
                walkDomain.Description = walk.Description;  
                walkDomain.WalkImageUrl = walk.WalkImageUrl;
                walkDomain.LengthInKm = walk.LengthInKm;
                walkDomain.DifficultyId = walk.DifficultyId;
                walkDomain.RegionId = walk.RegionId;
                //save to dB
                await dBContext.SaveChangesAsync();
                await dBContext.Entry(walkDomain).Reference(w => w.Difficulty).LoadAsync();
                await dBContext.Entry(walkDomain).Reference(w => w.Region).LoadAsync();
                return walkDomain;
            }
            return null;
        }
    }
}
