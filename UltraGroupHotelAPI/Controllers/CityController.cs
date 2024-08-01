using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetCities")]
        [ProducesResponseType(typeof(IEnumerable<CityVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CityVm>>> GetCities()
        {
            var query = new GetCitiesListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
