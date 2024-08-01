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

namespace UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(IMapper mapper, ILogger<CreateRoomCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            string roomId = $"P{request.Floor}-H{request.RoomNumber}";
            var hotelExist = await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.Id == request.HotelId);
            var roomTypeExist = await _unitOfWork.Repository<RoomType>().GetByTypeAsync(a => a.Id == request.RoomTypeId);
            var roomExist = await _unitOfWork.Repository<Room>().GetByTypeAsync(a => a.RoomId == roomId && a.HotelId == request.HotelId);

            if (hotelExist == null)
            {
                _logger.LogInformation($"El hotel {request.HotelId} no existe");
                throw new Exception($"El hotel {request.HotelId} no existe");
            }

            if (roomTypeExist == null)
            {
                _logger.LogInformation($"El tipo de habitacion {request.RoomTypeId} no existe");
                throw new Exception($"El tipo de habitacion {request.RoomTypeId} no existe");
            }

            if (roomExist != null)
            {
                _logger.LogInformation($"El registro {roomId} ya existe");
                throw new Exception($"El registro {roomId} ya existe");
            }

            var newRoom = Mapper(request);

            _unitOfWork.Repository<Room>().AddEntity(newRoom);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"No se pudo insertar el registro");
                throw new Exception("No se pudo insertar el registro");
            }

            _logger.LogInformation($"El registro {newRoom.RoomId} fue creado exitosamente");

            return newRoom.Id;
        }

        private Room Mapper(CreateRoomCommand command)
        {
            var hotel = new Room
            {
                RoomId = $"P{command.Floor}-H{command.RoomNumber}",
                Cost = command.Cost,
                BaseAmount = command.BaseAmount,
                Tax = command.Tax,
                Location = $"Piso {command.Floor} - Habitacion {command.RoomNumber}",
                Floor = command.Floor,
                RoomNumber = command.RoomNumber,
                Capacity = command.Capacity,
                IsAvailable = command.IsAvailable,
                IsEnabled = command.IsEnabled,
                RoomTypeId = command.RoomTypeId,
                HotelId = command.HotelId,
            };

            return hotel;
        }
    }
}
