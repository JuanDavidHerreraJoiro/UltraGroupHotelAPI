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

namespace UltraGroupHotelAPI.Application.Features.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomCommandHandler> _logger;

        public UpdateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateRoomCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            string roomId = $"P{request.Floor}-H{request.RoomNumber}";

            var hotelExist = await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.Id == request.HotelId);
            var roomTypeExist = await _unitOfWork.Repository<RoomType>().GetByTypeAsync(a => a.Id == request.RoomTypeId);
            var roomExist = await _unitOfWork.Repository<Room>().GetByTypeAsync(a => a.Id == request.Id && a.RoomId == roomId && a.HotelId == request.HotelId);

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

            if (roomExist == null)
            {
                _logger.LogError($"No se encontro el registro {roomId}");
                return false;
            }

            var roomToUpdate = Mapper(request, roomExist);

            _unitOfWork.Repository<Room>().UpdateEntity(roomToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"Operacion exitosa actualizando {roomId}");
            return true;
        }

        private Room Mapper(UpdateRoomCommand command, Room roomExist)
        {
            roomExist.RoomId = $"P{command.Floor}-H{command.RoomNumber}";
            roomExist.Cost = command.Cost;
            roomExist.BaseAmount = command.BaseAmount;
            roomExist.Tax = command.Tax;
            roomExist.Location = $"Piso {command.Floor} - Habitacion {command.RoomNumber}";
            roomExist.Floor = command.Floor;
            roomExist.RoomNumber = command.RoomNumber;
            roomExist.Capacity = command.Capacity;
            roomExist.IsAvailable = command.IsAvailable;
            roomExist.IsEnabled = command.IsEnabled;
            roomExist.RoomTypeId = command.RoomTypeId;
            roomExist.HotelId = command.HotelId;

            return roomExist;
        }
    }
}
