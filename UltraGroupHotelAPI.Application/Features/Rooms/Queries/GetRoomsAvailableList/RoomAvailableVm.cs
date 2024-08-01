using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;

namespace UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomAvailableList
{
    public class RoomAvailableVm
    {
        public int Id { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public double Cost { get; set; }
        public double BaseAmount { get; set; }
        public double Tax { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public RoomTypeVm? RoomType { get; set; }
        public HotelVm? Hotel { get; set; }
    }
}
