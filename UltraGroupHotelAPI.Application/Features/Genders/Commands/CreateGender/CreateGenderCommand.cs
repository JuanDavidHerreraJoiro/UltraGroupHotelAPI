using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Genders.Commands.CreateGender
{
    public class CreateGenderCommand : IRequest<int>
    {
        public string Type { get; set; } = string.Empty;
    }
}
