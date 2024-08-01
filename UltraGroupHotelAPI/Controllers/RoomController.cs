using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.UpdateRoom;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomAvailableList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomEnabledList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;

namespace UltraGroupHotelAPI.Controllers
{
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateRoom")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateRoom([FromBody] CreateRoomCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateRoom")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> UpdateRoom([FromBody] UpdateRoomCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetRooms")]
        [ProducesResponseType(typeof(IEnumerable<RoomVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoomVm>>> GetRooms()
        {
            var query = new GetRoomsListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }

        [HttpGet("GetRoomsAvailable")]
        [ProducesResponseType(typeof(IEnumerable<RoomAvailableVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoomAvailableVm>>> GetRoomsAvailable()
        {
            var query = new GetRoomsAvailableListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }

        [HttpGet("GetRoomsEnabled")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<RoomEnabledVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoomEnabledVm>>> GetRoomsEnabled()
        {
            var query = new GetRoomsEnabledListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
