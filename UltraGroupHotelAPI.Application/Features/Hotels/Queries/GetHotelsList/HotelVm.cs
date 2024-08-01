using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList
{
    public class HotelVm
    {
        public int Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public CityVm? City { get; set; }
        public List<RoomVm>? Rooms { get; set; }
        public string? UserId { get; set; } = string.Empty;
    }
}
