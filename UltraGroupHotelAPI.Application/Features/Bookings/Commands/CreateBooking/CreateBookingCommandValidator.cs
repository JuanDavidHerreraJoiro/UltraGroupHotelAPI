using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator() 
        {
            RuleFor(p => p.NumberPeople)
                .NotNull().WithMessage("{NumberPeople} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{NumberPeople} debe ser mayor a cero");

            RuleFor(p => p.EntryDate)
                .NotNull().WithMessage("{EntryDate} no puede estar en blanco");

            RuleFor(p => p.TravelerId)
                .NotNull().WithMessage("{TravelerId} no puede estar en blanco");

        }
    }
}
