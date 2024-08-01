using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Models.Identity;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Commands.CreateTraveller
{
    public class CreateTravelerCommand : IRequest<int>
    {
        public string Identification { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int GenderId { get; set; }
        public int DocumentTypeId { get; set; }
        public RegistrationRequest RegistrationRequest { get; set; }
    }
}
