using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Commands.CreateTraveller
{
    public class CreateTravelerCommandValidator : AbstractValidator<CreateTravelerCommand>
    {
        public CreateTravelerCommandValidator() 
        {
            RuleFor(p => p.Identification)
           .NotEmpty().WithMessage("{Identification} no puede estar en blanco")
           .Matches(@"^[0-9+() -]*$").WithMessage("{Identification} no debe contener letras")
           .NotNull();

            RuleFor(p => p.FirstName)
           .NotEmpty().WithMessage("{FirstName} no puede estar en blanco")
           .NotNull();

            RuleFor(p => p.LastName)
           .NotEmpty().WithMessage("{LastName} no puede estar en blanco")
           .NotNull();

            RuleFor(p => p.Birthday)
           .NotNull().WithMessage("{Birthday} no puede estar en blanco");

            RuleFor(p => p.Email)
           .NotEmpty().WithMessage("{Email} no puede estar en blanco")
           .NotNull();

            RuleFor(p => p.PhoneNumber)
           .NotEmpty().WithMessage("{PhoneNumber} no puede estar en blanco")
           .Matches(@"^[0-9+() -]*$").WithMessage("{PhoneNumber} no debe contener letras")
           .NotNull();

            RuleFor(p => p.GenderId)
           .NotNull().WithMessage("{FirstName} no puede estar en blanco");

            RuleFor(p => p.DocumentTypeId)
           .NotNull().WithMessage("{DocumentTypeId} no puede estar en blanco");

            RuleFor(p => p.RegistrationRequest)
            .NotNull().WithMessage("{RegistrationRequest} no puede estar en blanco");

        } 
    }
}
