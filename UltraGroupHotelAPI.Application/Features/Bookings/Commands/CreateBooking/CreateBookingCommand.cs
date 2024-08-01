using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Commands.CreateEmergencyContact;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int NumberPeople { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public int TravelerId { get; set; }
        public List<int>? Rooms { get; set; }
        public CreateEmergencyContactCommand EmergencyContact { get; set; }
    }
}
