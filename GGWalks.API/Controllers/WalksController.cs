using GGWalks.API.CustomActionFilters;
using GGWalks.API.Models.Domain;
using GGWalks.API.Models.DTO;
using GGWalks.API.Models.DTO.Walk;
using GGWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace GGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly ILogger<WalksController> logger;

        public WalksController(IWalkRepository walkRepository, ILogger<WalksController> logger)
        {
            this.walkRepository = walkRepository;
            this.logger = logger;
        }

        //create Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // map dto to domain
            var walkDomain = new Walk
            {
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImageUrl = addWalkRequestDto.WalkImageUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId

            };
            //repo stuff

            walkDomain = await walkRepository.CreateAsync(walkDomain);

            //domain to dto
            var walkDto = new WalkResponseDto
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Description = walkDomain.Description,
                LengthInKm = walkDomain.LengthInKm,
                WalkImageUrl = walkDomain.WalkImageUrl,
                Difficulty = new DifficultyDto
                {
                    Id = walkDomain.Difficulty.Id,
                    Name = walkDomain.Difficulty.Name
                },
                Region = new RegionDto
                {
                    Id = walkDomain.Region.Id,
                    Code = walkDomain.Region.Code,
                    Name = walkDomain.Region.Name,
                    RegionImageUrl = walkDomain.Region.RegionImageUrl
                }
            };
            return Ok(walkDto);
        }

        // Get-All Walks
        [HttpGet]
        public async Task<IActionResult> GetWalk([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomain = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            //domain to dto
            var walkDto = new List<WalkResponseDto>();
            foreach (var walk in walkDomain)
            {
                walkDto.Add(new WalkResponseDto
                {
                    Id = walk.Id,
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    Difficulty = new DifficultyDto
                    {
                        Id = walk.Difficulty.Id,
                        Name = walk.Difficulty.Name
                    },
                    Region = new RegionDto
                    {
                        Id = walk.Region.Id,
                        Code = walk.Region.Code,
                        Name = walk.Region.Name,
                        RegionImageUrl = walk.Region.RegionImageUrl
                    }
                });
            }
            return Ok(walkDto);
        }

        //Get Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id) 
        {
            var walkDomain = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomain != null)
            {
                // domain to dto 
                var walkDto = new WalkResponseDto
                {
                    Id = walkDomain.Id,
                    Name = walkDomain.Name,
                    Description = walkDomain.Description,
                    LengthInKm = walkDomain.LengthInKm,
                    WalkImageUrl = walkDomain.WalkImageUrl,
                    Difficulty = new DifficultyDto
                    {
                        Id = walkDomain.Difficulty.Id,
                        Name = walkDomain.Difficulty.Name
                    },
                    Region = new RegionDto
                    {
                        Id = walkDomain.Region.Id,
                        Code = walkDomain.Region.Code,
                        Name = walkDomain.Region.Name,
                        RegionImageUrl = walkDomain.Region.RegionImageUrl
                    }
                };
                return Ok(walkDto);

            }
            return NotFound();
        }

        //Update walk by id
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkById([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //dto to domain
            var walkDomain = new Walk
            {
                Name = updateWalkRequestDto.Name,
                Description = updateWalkRequestDto.Description,
                WalkImageUrl = updateWalkRequestDto.WalkImageUrl,
                LengthInKm = updateWalkRequestDto.LengthInKm,
                DifficultyId = updateWalkRequestDto.DifficultyId,
                RegionId = updateWalkRequestDto.RegionId
            };
            walkDomain = await walkRepository.UpdateWalkByIdAsync(id, walkDomain);
            if (walkDomain != null)
            {
                //domain to dto
                var walkDto = new WalkResponseDto
                {
                    Id = walkDomain.Id,
                    Name = walkDomain.Name,
                    Description = walkDomain.Description,
                    LengthInKm = walkDomain.LengthInKm,
                    WalkImageUrl = walkDomain.WalkImageUrl,
                    Difficulty = new DifficultyDto
                    {
                        Id = walkDomain.Difficulty.Id,
                        Name = walkDomain.Difficulty.Name
                    },
                    Region = new RegionDto
                    {
                        Id = walkDomain.Region.Id,
                        Code = walkDomain.Region.Code,
                        Name = walkDomain.Region.Name,
                        RegionImageUrl = walkDomain.Region.RegionImageUrl
                    }
                };
                return Ok(walkDto);
            }
            return NotFound();
        }

        //Delete Walk by id
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkById([FromRoute]Guid id)
        {
            var walkDomain = await walkRepository.DeleteWalkByIdAsync(id);

            // domain to dto
            if (walkDomain != null)
            {
                var walkDto = new WalkResponseDto
                {
                    Id = walkDomain.Id,
                    Name = walkDomain.Name,
                    Description = walkDomain.Description,
                    LengthInKm = walkDomain.LengthInKm,
                    WalkImageUrl = walkDomain.WalkImageUrl,
                    Difficulty = new DifficultyDto
                    {
                        Id = walkDomain.Difficulty.Id,
                        Name = walkDomain.Difficulty.Name
                    },
                    Region = new RegionDto
                    {
                        Id = walkDomain.Region.Id,
                        Code = walkDomain.Region.Code,
                        Name = walkDomain.Region.Name,
                        RegionImageUrl = walkDomain.Region.RegionImageUrl
                    }
                };
                return Ok(walkDto);
            }
            return NotFound();
        }
    }
}
