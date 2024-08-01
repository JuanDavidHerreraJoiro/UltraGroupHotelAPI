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
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Commands.CreateEmergencyContact;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.UpdateRoom;
using UltraGroupHotelAPI.Application.Models.Email;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _emailService;

        public CreateBookingCommandHandler(IMapper mapper, ILogger<CreateBookingCommandHandler> logger, IUnitOfWork unitOfWork, IEmailServices emailService)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var travelerExist = await _unitOfWork.Repository<Traveler>().GetByTypeAsync(a => a.Id == request.TravelerId);

            if (travelerExist == null)
            {
                _logger.LogInformation($"El viajero {request.TravelerId} no existe");
                throw new Exception($"El viajero {request.TravelerId} no existe");
            }

            List<int> roomsNotAvailable = new List<int>();
            List<Room> roomsAvailable = new List<Room>();

            foreach (var item in request.Rooms)
            {
                var roomExist = await _unitOfWork.Repository<Room>().GetByTypeAsync(a => a.Id == item && a.IsEnabled == true && a.IsAvailable == true);

                if (roomExist == null)
                {
                    roomsNotAvailable.Add(item);
                }
                else
                {
                    roomsAvailable.Add(roomExist);
                }
            }

            if (roomsNotAvailable != null )
            {
                if (roomsNotAvailable.Count > 0)
                {
                    string roomsNotAvailableString = string.Join(" - ", roomsNotAvailable);
                    _logger.LogInformation($"La habitacion o habitaciobes {roomsNotAvailableString} no estan disponibles");
                    throw new Exception($"La habitacion o habitaciobes {roomsNotAvailableString} no estan disponibles");
                }
            }

            var newEmergencyContact = MapperEmergencyContact(request.EmergencyContact);
            _unitOfWork.Repository<EmergencyContact>().AddEntity(newEmergencyContact);

            var resultEmergencyContact = await _unitOfWork.Complete();

            if (resultEmergencyContact <= 0)
            {
                _logger.LogError($"No se pudo registrar el contacto de emergencia {request.EmergencyContact.FirstName} {request.EmergencyContact.LastName}");
                throw new Exception($"No se pudo registrar el contacto de emergencia {request.EmergencyContact.FirstName} {request.EmergencyContact.LastName}");
            }

            var newBooking = Mapper(request, newEmergencyContact.Id);

            _unitOfWork.Repository<Booking>().AddEntity(newBooking);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError($"No se pudo insertar el registro");
                throw new Exception("No se pudo insertar el registro");
            }

            //Actualizar habitaciones
            foreach (var item in roomsAvailable)
            {
                var roomToUpdate = UpdateRoom(item, newBooking.Id);

                _unitOfWork.Repository<Room>().UpdateEntity(roomToUpdate);

                await _unitOfWork.Complete();
            }

            await SendEmail(travelerExist);
            _logger.LogInformation($"El registro {newBooking.Id} fue creado exitosamente");

            return newBooking.Id;
        }

        private async Task SendEmail(Traveler traveler)
        {
            var email = new Email
            {
                To = traveler.Email,
                Body = "The booking has been created successfully.",
                Subject = "¡Message Security!"
            };

            try
            {
                await _emailService.SendEmailAsync(email, traveler);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errores enviendo el email de {traveler.FirstName} {traveler.LastName} : {traveler.Identification}");
            }
        }

        private Room UpdateRoom(Room roomExist, int BookingId)
        {
            roomExist.IsAvailable = false;
            roomExist.BookingId = BookingId;

            return roomExist;
        }

        private EmergencyContact MapperEmergencyContact(CreateEmergencyContactCommand command)
        {
            var emergencyContact = new EmergencyContact
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber,
            };

            return emergencyContact;
        }

        private Booking Mapper(CreateBookingCommand command, int EmergencyContactId)
        {
            var booking = new Booking
            {
                NumberPeople = command.NumberPeople,
                EntryDate = command.EntryDate,
                ExitDate = command.ExitDate,
                EmergencyContactId = EmergencyContactId,
                TravelerId = command.TravelerId,
            };

            return booking;
        }

    }
}
