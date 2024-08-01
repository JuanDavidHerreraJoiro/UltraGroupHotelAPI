using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom;
using UltraGroupHotelAPI.Application.Features.Travellers.Commands.CreateTraveller;
using UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TravelerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TravelerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateTraveler")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateTraveler([FromBody] CreateTravelerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetTravellers")]
        [ProducesResponseType(typeof(IEnumerable<TravelerVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TravelerVm>>> GetTravellers()
        {
            var query = new GetTravellersListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
