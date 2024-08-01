using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList
{
    public class GetGendersListQuery : IRequest<List<GenderVm>>
    {
    }
}
