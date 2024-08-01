using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Queries.GetEmergencyContactList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList
{
    public class BookingVm
    {
        public int Id { get; set; }
        public int NumberPeople { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public EmergencyContactVm? EmergencyContact { get; set; }
        public TravelerVm? Traveler { get; set; }
        public List<RoomVm>? Rooms { get; set; }
    }
}
