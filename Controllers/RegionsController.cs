using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalkAPP.Data;
using WalkAPP.DTO;
using WalkAPP.Model.Domain;

namespace WalkAPP.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly WalkAppDbContext dbContext;

		public RegionsController(WalkAppDbContext dbContext)
        {
			this.dbContext = dbContext;
		}
        [HttpGet]
		public IActionResult GetAll()
		{
			var regionsDomain = dbContext.Regions.ToList();

			var regionsDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = regionDomain.Id,
					Name = regionDomain.Name,
					Code = regionDomain.Code,
					RegionImageUrl = regionDomain.RegionImageUrl,
				});

			}

			return Ok(regionsDto);

		}

		//GET SINGLE REGION
		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			//var region = dbContext.Regions.Find(id);
			var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
			if(regionDomain == null)
			{
				return NotFound();
			}
			var regionDto = new RegionDto()
			{
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
			return Ok(regionDto);
        }
		[HttpPost]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			//Map or convert the DTO to domain model
			var regionDomainModel = new Region
			{
				
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};
			 dbContext.Regions.Add(regionDomainModel);
			dbContext.SaveChanges();

			var regionDto = new RegionDto()
            {
				Id = regionDomainModel.Id,
				Name = regionDomainModel.Name,
              Code = regionDomainModel.Code,
			  RegionImageUrl = regionDomainModel.RegionImageUrl,

            };

		
			return CreatedAtAction(nameof(GetById), new {id = regionDto.Id }, regionDto);
        }
	}
}
