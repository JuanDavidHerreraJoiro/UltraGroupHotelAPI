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
using UltraGroupHotelAPI.Application.Features.Genders.Commands.CreateGender;
using UltraGroupHotelAPI.Application.Models.Identity;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Domain.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHotelCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateHotelCommandHandler(IMapper mapper, ILogger<CreateHotelCommandHandler> logger, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var isExistUser = await _authService.Register(request.RegistrationRequest,EnumRoles.Administrator);

            if (isExistUser.Errors != null)
            {
                string erroresConcatenados = "";
                foreach (string error in isExistUser.Errors)
                {
                    erroresConcatenados += error + ". ";
                }

                throw new Exception($"{erroresConcatenados}");
            }

            var isExist = await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.HotelName == request.HotelName);
            var cityExist = await _unitOfWork.Repository<City>().GetByTypeAsync(a => a.Id == request.CityId);

            if (cityExist == null)
            {
                _logger.LogInformation($"La ciudad {request.CityId} no existe");
                throw new Exception($"La ciudad {request.CityId} no existe");
            }

            if (isExist != null)
            {
                _logger.LogInformation($"El registro {request.HotelName} ya existe");
                throw new Exception($"El registro {request.HotelName} ya existe");
            }

            var newhotel = Mapper(request,(RegistrationResponse) isExistUser.Data);

            _unitOfWork.Repository<Hotel>().AddEntity(newhotel);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"No se pudo insertar el registro");
                throw new Exception("No se pudo insertar el registro");
            }

            _logger.LogInformation($"El registro {newhotel.HotelName} fue creado exitosamente");

            return newhotel.Id;
        }

        private Hotel Mapper(CreateHotelCommand command, RegistrationResponse registrationResponse)
        {
            var hotel = new Hotel
            {
                HotelName = command.HotelName.ToUpper(),
                IsEnabled = command.IsEnabled,
                CityId  = command.CityId,
                UserId = registrationResponse.Id
            };

            return hotel;
        }
    }
}
