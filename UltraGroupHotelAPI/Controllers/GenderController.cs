using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetGenders")]
        [ProducesResponseType(typeof(IEnumerable<GenderVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<GenderVm>>> GetGenders()
        {
            var query = new GetGendersListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
