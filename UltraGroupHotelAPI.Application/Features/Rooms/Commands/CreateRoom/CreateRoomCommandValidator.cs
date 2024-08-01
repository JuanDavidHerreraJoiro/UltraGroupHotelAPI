using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(p => p.Cost)
                .NotNull().WithMessage("{Cost} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{Cost} debe ser mayor que cero");

            RuleFor(p => p.BaseAmount)
                .NotNull().WithMessage("{BaseAmount} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{BaseAmount} debe ser mayor que cero");

            RuleFor(p => p.Tax)
                .NotNull().WithMessage("{Tax} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{Tax} debe ser mayor que cero");

            RuleFor(p => p.Floor)
                .NotNull().WithMessage("{Floor} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{Floor} debe ser mayor que cero");

            RuleFor(p => p.RoomNumber)
                .NotNull().WithMessage("{RoomNumber} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{RoomNumber} debe ser mayor que cero");

            RuleFor(p => p.Capacity)
                .NotNull().WithMessage("{Capacity} no puede estar en blanco")
                .GreaterThan(0).WithMessage("{Capacity} debe ser mayor que cero");

            RuleFor(p => p.IsAvailable)
                .NotNull().WithMessage("{IsAvailable} no puede estar en blanco - true o false");

            RuleFor(p => p.IsEnabled)
                .NotNull().WithMessage("{IsEnabled} no puede estar en blanco - true o false");

            RuleFor(p => p.RoomTypeId)
                .NotNull().WithMessage("{RoomTypeId} no puede estar en blanco");

            RuleFor(p => p.HotelId)
                .NotNull().WithMessage("{HotelId} no puede estar en blanco");

        }
    }
}
