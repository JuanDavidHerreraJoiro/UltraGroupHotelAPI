using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList
{
    public class GetCitiesListQuery : IRequest<List<CityVm>>
    {
    }
}
