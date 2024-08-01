using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList;
using UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList
{
    public class TravelerVm
    {
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public GenderVm? Gender { get; set; }
        public DocumentTypeVm? DocumentType { get; set; }
        public List<BookingVm>? Bookings { get; set; }
        public string? UserId { get; set; } = string.Empty;
    }
}
