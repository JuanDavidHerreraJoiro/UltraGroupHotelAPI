using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(p => p.HotelName)
            .NotEmpty().WithMessage("{HotelName} no puede estar en blanco")
            .NotNull();

            RuleFor(p => p.IsEnabled)
           .NotNull().WithMessage("{IsEnabled} no puede estar en blanco true o false");

            RuleFor(p => p.CityId)
            .NotNull().WithMessage("{CityId} no puede estar en blanco");

            RuleFor(p => p.RegistrationRequest)
            .NotNull().WithMessage("{RegistrationRequest} no puede estar en blanco");
        }
    }
}
