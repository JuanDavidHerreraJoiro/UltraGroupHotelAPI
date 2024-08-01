using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Hotels.Commands.CreateHotel;
using UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelRoomsEnabledList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;

namespace UltraGroupHotelAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateHotel")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateHotel([FromBody] CreateHotelCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("UpdateHotel")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> UpdateHotel([FromBody] UpdateHotelCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [HttpGet("GetHotels")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<HotelVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelVm>>> GetHotels()
        {
            var query = new GetHotelsListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }

        [HttpGet("GetHotelRoomsEnabled")]
        [ProducesResponseType(typeof(IEnumerable<HotelEnabledVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelEnabledVm>>> GetHotelRoomsEnabled(int HotelId)
        {
            var query = new GetHotelRoomsEnabledListQuery(HotelId);

            var list = await _mediator.Send(query);

            return Ok(list);
        }

        [HttpGet("GetHotelsEnabled")]
        [ProducesResponseType(typeof(IEnumerable<HotelEnabledVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<HotelEnabledVm>>> GetHotelsEnabled()
        {
            var query = new GetHotelsEnabledListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
