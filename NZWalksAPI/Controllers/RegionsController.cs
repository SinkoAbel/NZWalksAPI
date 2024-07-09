using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Requests;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Region> regions = await dbContext.Regions.ToListAsync();
            List<RegionDto> regionsDto = new List<RegionDto>();

            foreach (Region region in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            Region? region = await dbContext.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            RegionDto regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto request)
        {
            Region regionDomain = new Region
            {
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl,
            };

            await dbContext.Regions.AddAsync(regionDomain);
            await dbContext.SaveChangesAsync();

            RegionDto regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = regionDomain.Id },
                regionDto
            );
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionRequestDto request, [FromRoute] Guid id)
        {
            Region? foundRegion = await dbContext.Regions.FindAsync(id);

            if (foundRegion == null)
            {
                return NotFound();
            }

            foundRegion.Code = request.Code ?? foundRegion.Code;
            foundRegion.Name = request.Name ?? foundRegion.Name;
            foundRegion.RegionImageUrl = request.RegionImageUrl;

            dbContext.Regions.Update(foundRegion);
            await dbContext.SaveChangesAsync();

            RegionDto regionDto = new RegionDto
            {
                Id = foundRegion.Id,
                Code = foundRegion.Code,
                Name = foundRegion.Name,
                RegionImageUrl = foundRegion.RegionImageUrl,
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = foundRegion.Id },
                regionDto
            );
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            Region? foundRegion = await dbContext.Regions.FindAsync(id);
            
            if (foundRegion == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(foundRegion);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
