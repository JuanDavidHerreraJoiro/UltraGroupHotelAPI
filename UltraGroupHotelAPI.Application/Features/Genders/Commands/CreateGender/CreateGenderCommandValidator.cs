using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Genders.Commands.CreateGender
{
    public class CreateGenderCommandValidator : AbstractValidator<CreateGenderCommand>
    {
        public CreateGenderCommandValidator() 
        {
            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{Type} no puede estar en blanco")
                .NotNull();
        }
    }
}
