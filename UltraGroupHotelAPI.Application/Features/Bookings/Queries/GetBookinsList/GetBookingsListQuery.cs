using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList
{
    public class GetBookingsListQuery : IRequest<List<BookingVm>>
    {
    }
}
