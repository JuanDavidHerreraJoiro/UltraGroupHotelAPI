using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList
{
    public class GetRoomTypesListQuery : IRequest<List<RoomTypeVm>>
    {
    }
}
