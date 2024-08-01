using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList
{
    public class GetTravellersListQuery : IRequest<List<TravelerVm>>
    {
    }
}
