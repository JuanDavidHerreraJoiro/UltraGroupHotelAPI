using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookingDynamicFilter;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateBooking")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateBooking([FromBody] CreateBookingCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetBookings")]
        [ProducesResponseType(typeof(IEnumerable<BookingVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BookingVm>>> GetBookings()
        {
            var query = new GetBookingsListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }

        [HttpGet("GetBookingsDynamicFilter")]
        [ProducesResponseType(typeof(IEnumerable<BookingDynamicFilterVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BookingDynamicFilterVm>>> GetBookingsDynamicFilter(DateTime? EntryDate, DateTime? ExitDate, int? Capacity, int? CityId)
        {
            var query = new GetBookingsDynamicFilterListQuery(EntryDate, ExitDate, Capacity, CityId);

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
