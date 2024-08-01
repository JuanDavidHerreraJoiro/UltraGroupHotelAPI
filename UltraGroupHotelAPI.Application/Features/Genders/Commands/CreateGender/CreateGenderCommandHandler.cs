using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Infrastructure;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Genders.Commands.CreateGender
{
    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateGenderCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGenderCommandHandler(IMapper mapper, ILogger<CreateGenderCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var genderExist = await _unitOfWork.Repository<Gender>().GetByTypeAsync(a => a.Type == request.Type);

            if (genderExist != null)
            {
                _logger.LogInformation($"El registro {request.Type} ya existe");
                throw new Exception($"El registro {request.Type} ya existe");
            }

            var newGender = Mapper(request);

            _unitOfWork.Repository<Gender>().AddEntity(newGender);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"No se pudo insertar el registro");
                throw new Exception("No se pudo insertar el registro");
            }

            _logger.LogInformation($"El registro {newGender.Type} fue creado exitosamente");

            return newGender.Id;
        }

        private Gender Mapper(CreateGenderCommand command)
        {
            var gender = new Gender
            {
                Type = command.Type,
            };

            return gender;
        }
    }
}
