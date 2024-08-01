using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Models.Identity;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommand : IRequest<int>
    {
        public string HotelName { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public int CityId { get; set; }
        public RegistrationRequest RegistrationRequest { get; set; }
    }
}
