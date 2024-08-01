using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Queries.GetEmergencyContactList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookingDynamicFilter
{
    public class BookingDynamicFilterVm
    {
        public List<RoomVm>? Rooms { get; set; }
    }
}
