using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList
{
    public class GetHotelsEnabledListQuery : IRequest<List<HotelEnabledVm>>
    {
    }
}
