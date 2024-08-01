using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList
{
    public class GetHotelsListQuery : IRequest<List<HotelVm>>
    {
    }
}
