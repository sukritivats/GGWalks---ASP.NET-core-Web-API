using Azure.Core.Serialization;
using GGWalks.API.CustomActionFilters;
using GGWalks.API.Data;
using GGWalks.API.Models.Domain;
using GGWalks.API.Models.DTO;
using GGWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GGWalks.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class RegionsController : ControllerBase
    {
        private readonly GGWalksDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(GGWalksDBContext dbContext, IRegionRepository regionRepository, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }

        // GET ALL Regions
        // GET: https://localhost:7106/api/regions
        // versioning- https://localhost:7106/api/regions?api-version=1.0
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            // get region domain model from DB
            //var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllRegionAsync();
            // convert domain model to dTO
            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            //return DTO to client
            return Ok(regionDto);

        }

        // GET ALL Regions By ID
        // GET: https://localhost:7106/api/regions/{id}
        [HttpGet]
        [Authorize(Roles = "Reader")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            // get data from DB - domain  model
            //var region = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);
            if (regionDomain != null)
            {
                // convert domain to DTO
                var regionDTO = new List<RegionDto>();
                regionDTO.Add(new RegionDto
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
                //return DTO to client
                return Ok(regionDTO);
            }
            return NotFound();
        }

        // Create Region
        // POST: https://localhost:7106/api/regions
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async  Task<IActionResult> CreateRegions([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // map DTO to domain model
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //create new region using domain model with help of DB context
            //await dbContext.Regions.AddAsync(regionDomain);
            //await dbContext.SaveChangesAsync();
            regionDomain = await regionRepository.CreateRegionAsync(regionDomain);

            //map domain back to dTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //return dto to client
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }

        // Update Region by Id
        // PUT: https://localhost:7106/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRegionById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if id exists
            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            regionDomain = await regionRepository.UpdateRegionByIdAsync(id, regionDomain);
            if (regionDomain != null)
            {
                ////update 
                //regionDomain.Code = updateRegionRequestDto.Code;
                //regionDomain.Name = updateRegionRequestDto.Name;
                //regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
                ////save changes to dB
                //await dbContext.SaveChangesAsync(); -- in repo

                //map domain to dto
                var regionDto = new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                };
                //send dto to client
                return Ok(regionDto);
            }
            return NotFound();
        }

        // Delete Region By Id
        // DELETE: https://localhost:7106/api/regions/{id}

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> DeleteRegionById([FromRoute] Guid id)
        {
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.DeleteRegionByIdAsync(id);
            if (regionDomain != null)
            {
                //dbContext.Regions.Remove(regionDomain);
                //await dbContext.SaveChangesAsync();
                // domain to dto

                var regionDto = new RegionDto
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                };
                return Ok(regionDto);
                //return Ok(new { Message = "Deleted Region", Data = regionDto });
                //var jsonString = JsonSerializer.Serialize(regionDto);
                //return Ok($"Deleted Region : {jsonString}");
            }
            return NotFound();

            //int rowsDeleted = await dbContext.Regions.Where(x => x.Id == id).ExecuteDeleteAsync();
            //if(rowsDeleted > 0)
            //    return Ok("Region Deleted Successfully");
            //return NotFound();
        }
    
    }
}
