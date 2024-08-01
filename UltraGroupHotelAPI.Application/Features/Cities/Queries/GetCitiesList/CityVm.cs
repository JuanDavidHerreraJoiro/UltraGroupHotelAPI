using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList
{
    public class CityVm
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
