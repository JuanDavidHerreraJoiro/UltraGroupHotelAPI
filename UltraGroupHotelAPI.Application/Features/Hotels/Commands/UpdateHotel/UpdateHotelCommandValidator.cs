using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(p => p.HotelName)
            .NotEmpty().WithMessage("{HotelName} no puede estar en blanco")
            .NotNull();

            RuleFor(p => p.IsEnabled)
            .NotNull().WithMessage("{IsEnabled} no puede estar en blanco true o false");

            RuleFor(p => p.CityId)
            .NotNull().WithMessage("{CityId} no puede estar en blanco");
        }
    }
}
