using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelRoomsEnabledList
{
    public class GetHotelRoomsEnabledListQuery : IRequest<HotelRoomsEnabledVm>
    {
        public int HotelId { get; set; }

        public GetHotelRoomsEnabledListQuery(int hotelId)
        {
            HotelId = hotelId;
        }
    }
}
