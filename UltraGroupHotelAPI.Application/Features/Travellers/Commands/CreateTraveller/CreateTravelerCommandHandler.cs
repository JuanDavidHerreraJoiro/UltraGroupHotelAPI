using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Identity;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Models.Identity;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Domain.Enum;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Commands.CreateTraveller
{
    public class CreateTravelerCommandHandler : IRequestHandler<CreateTravelerCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTravelerCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateTravelerCommandHandler(IMapper mapper, ILogger<CreateTravelerCommandHandler> logger, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> Handle(CreateTravelerCommand request, CancellationToken cancellationToken)
        {
            var isExistUser = await _authService.Register(request.RegistrationRequest, EnumRoles.Traveler);

            if (isExistUser.Errors != null)
            {
                string erroresConcatenados = "";
                foreach (string error in isExistUser.Errors)
                {
                    erroresConcatenados += error + ". ";
                }

                throw new Exception($"{erroresConcatenados}");
            }

            var genderExist = await _unitOfWork.Repository<Gender>().GetByTypeAsync(a => a.Id == request.GenderId);
            var documentTypeExist = await _unitOfWork.Repository<DocumentType>().GetByTypeAsync(a => a.Id == request.DocumentTypeId);
            var isExist = await _unitOfWork.Repository<Traveler>().GetByTypeAsync(a => a.Identification == request.Identification);

            if (genderExist == null)
            {
                _logger.LogInformation($"El genero {request.GenderId} no existe");
                throw new Exception($"El genero {request.GenderId} no existe");
            }

            if (documentTypeExist == null)
            {
                _logger.LogInformation($"El tipo de documento {request.DocumentTypeId} no existe");
                throw new Exception($"El tipo de documento {request.DocumentTypeId} no existe");
            }

            if (isExist != null)
            {
                _logger.LogInformation($"El registro {request.Identification} ya existe");
                throw new Exception($"El registro {request.Identification} ya existe");
            }

            var newTraveler = Mapper(request, (RegistrationResponse)isExistUser.Data);

            _unitOfWork.Repository<Traveler>().AddEntity(newTraveler);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"No se pudo insertar el registro");
                throw new Exception("No se pudo insertar el registro");
            }

            _logger.LogInformation($"El registro {newTraveler.Identification} fue creado exitosamente");

            return newTraveler.Id;
        }

        private Traveler Mapper(CreateTravelerCommand command, RegistrationResponse registrationResponse)
        {
            var traveler = new Traveler
            {
                Identification = command.Identification,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Birthday = command.Birthday,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                GenderId = command.GenderId,
                DocumentTypeId = command.DocumentTypeId,
                UserId = registrationResponse.Id
            };

            return traveler;
        }
    }
}
