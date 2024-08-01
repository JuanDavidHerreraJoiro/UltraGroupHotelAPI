using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;

        public UpdateHotelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateHotelCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var personToUpdate = await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.Id == request.Id);

            var cityExist = await _unitOfWork.Repository<City>().GetByTypeAsync(a => a.Id == request.CityId);

            if (cityExist == null)
            {
                _logger.LogInformation($"La ciudad {request.CityId} no existe");
                throw new Exception($"Ls ciudad {request.CityId} no existe");
            }

            if (personToUpdate == null)
            {
                _logger.LogError($"No se encontro el registro {request.HotelName}");
                return false;
            }

            _mapper.Map(request, personToUpdate, typeof(UpdateHotelCommand), typeof(Hotel));

            _unitOfWork.Repository<Hotel>().UpdateEntity(personToUpdate);
            await _unitOfWork.Complete();

            _logger.LogInformation($"Operacion exitosa actualizando {request.HotelName}");
            return true;
        }
    }
}
