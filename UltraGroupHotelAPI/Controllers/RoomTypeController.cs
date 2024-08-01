using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoomTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetRoomTypes")]
        [ProducesResponseType(typeof(IEnumerable<RoomTypeVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoomTypeVm>>> GetRoomTypes()
        {
            var query = new GetRoomTypesListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
