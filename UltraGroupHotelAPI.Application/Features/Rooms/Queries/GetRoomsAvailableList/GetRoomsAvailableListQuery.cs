using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomAvailableList
{
    public class GetRoomsAvailableListQuery : IRequest<List<RoomAvailableVm>>
    {
    }
}
