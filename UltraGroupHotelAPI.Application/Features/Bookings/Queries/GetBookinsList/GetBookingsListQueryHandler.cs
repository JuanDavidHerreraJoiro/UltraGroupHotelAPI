using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Queries.GetEmergencyContactList;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList
{
    public class GetBookingsListQueryHandler : IRequestHandler<GetBookingsListQuery, List<BookingVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBookingsListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BookingVm>> Handle(GetBookingsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Booking>().GetAllAsync();

            List<BookingVm> bookingVmList = new List<BookingVm>();

            foreach (var user in list)
            {
                bookingVmList.Add(await Mapper(user));
            }

            return bookingVmList;
        }

        private async Task<BookingVm> Mapper(Booking command)
        {
            var bookingVm = new BookingVm
            {
                Id = command.Id,
                NumberPeople = command.NumberPeople,
                EntryDate = command.EntryDate,
                ExitDate = command.ExitDate,
                EmergencyContact = MapperEmergencyContact(await _unitOfWork.Repository<EmergencyContact>().GetByTypeAsync(a => a.Id == command.EmergencyContactId)),
                Traveler = await MapperTraveler(await _unitOfWork.Repository<Traveler>().GetByTypeAsync(a => a.Id == command.TravelerId)),
                Rooms = await ListRoomVm(command.Id),
            };

            return bookingVm;
        }

        private async Task<List<RoomVm>> ListRoomVm(int BookingId)
        {
            var listcommand = await _unitOfWork.Repository<Room>().GetAsync(a => a.BookingId == BookingId);
            List<RoomVm> roomVmList = new List<RoomVm>();

            foreach (var command in listcommand)
            {
                var roomVm = new RoomVm
                {
                    Id = command.Id,
                    RoomId = command.RoomId,
                    Cost = command.Cost,
                    BaseAmount = command.BaseAmount,
                    Tax = command.Tax,
                    Location = command.Location,
                    Floor = command.Floor,
                    RoomNumber = command.RoomNumber,
                    Capacity = command.Capacity,
                    IsAvailable = command.IsAvailable,
                    IsEnabled = command.IsEnabled,
                    RoomType = MapperRoomType(await _unitOfWork.Repository<RoomType>().GetByTypeAsync(a => a.Id == command.RoomTypeId)),
                    Hotel = await MapperHotel(await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.Id == command.HotelId))

                };

                roomVmList.Add(roomVm);
            }

            return roomVmList;
        }

        private async Task<HotelVm> MapperHotel(Hotel command)
        {
            var hotelVm = new HotelVm
            {
                Id = command.Id,
                HotelName = command.HotelName,
                IsEnabled = command.IsEnabled,
                City = MapperCity(await _unitOfWork.Repository<City>().GetByTypeAsync(a => a.Id == command.CityId)),
                UserId = command.UserId
            };

            return hotelVm;
        }

        private CityVm MapperCity(City command)
        {
            var roomTypeVm = new CityVm
            {
                Id = command.Id,
                CityName = command.CityName,
            };

            return roomTypeVm;
        }

        private RoomTypeVm MapperRoomType(RoomType command)
        {
            var roomTypeVm = new RoomTypeVm
            {
                Id = command.Id,
                Type = command.Type,
                Description = command.Description,
            };

            return roomTypeVm;
        }

        private EmergencyContactVm MapperEmergencyContact(EmergencyContact command)
        {
            var emergencyContactVm = new EmergencyContactVm
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber
            };

            return emergencyContactVm;
        }

        private async Task<TravelerVm> MapperTraveler(Traveler command)
        {
            var travelerVm = new TravelerVm
            {
                Id = command.Id,
                Identification = command.Identification,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Birthday = command.Birthday,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Gender = MapperGender(await _unitOfWork.Repository<Gender>().GetByTypeAsync(a => a.Id == command.GenderId)),
                DocumentType = MapperDocumentType(await _unitOfWork.Repository<DocumentType>().GetByTypeAsync(a => a.Id == command.DocumentTypeId)),
                UserId = command.UserId
            };

            return travelerVm;
        }

        private GenderVm MapperGender(Gender command)
        {
            var genderVm = new GenderVm
            {
                Id = command.Id,
                Type = command.Type,
            };

            return genderVm;
        }

        private DocumentTypeVm MapperDocumentType(DocumentType command)
        {
            var documentTypeVm = new DocumentTypeVm
            {
                Id = command.Id,
                Type = command.Type,
            };

            return documentTypeVm;
        }
    }
}
