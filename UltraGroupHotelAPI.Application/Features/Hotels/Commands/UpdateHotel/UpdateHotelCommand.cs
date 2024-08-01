using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommand : IRequest<bool>
    {
        public int? Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public int CityId { get; set; }
    }
}
