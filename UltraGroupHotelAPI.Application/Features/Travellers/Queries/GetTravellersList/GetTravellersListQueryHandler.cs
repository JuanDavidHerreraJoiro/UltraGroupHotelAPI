using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Commands.CreateEmergencyContact;
using UltraGroupHotelAPI.Application.Features.EmergencyContacts.Queries.GetEmergencyContactList;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList
{
    public class GetTravellersListQueryHandler : IRequestHandler<GetTravellersListQuery, List<TravelerVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTravellersListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TravelerVm>> Handle(GetTravellersListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Traveler>().GetAllAsync();

            List<TravelerVm> hotelsVmList = new List<TravelerVm>();

            foreach (var item in list)
            {
                hotelsVmList.Add(await Mapper(item));
            }

            return hotelsVmList;
        }

        private async Task<TravelerVm> Mapper(Traveler command)
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
                Bookings = await ListBookingVm(command.Id),
                UserId = command.UserId

            };

            return travelerVm;
        }

        private async Task<List<BookingVm>> ListBookingVm(int TravelerId)
        {
            var listcommand = await _unitOfWork.Repository<Booking>().GetAsync(a => a.TravelerId == TravelerId);
            List<BookingVm> bookingVmList = new List<BookingVm>();

            foreach (var command in listcommand)
            {
                var bookingVm = new BookingVm
                {
                    Id = command.Id,
                    NumberPeople = command.NumberPeople,
                    EntryDate = command.EntryDate,
                    ExitDate    = command.ExitDate,
                    EmergencyContact = MapperEmergencyContact(await _unitOfWork.Repository<EmergencyContact>().GetByTypeAsync(a => a.Id == command.EmergencyContactId)),
                    //Rooms = await ListRoomVm(command.Id),
                };

                bookingVmList.Add(bookingVm);
            }

            return bookingVmList;
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

        private GenderVm MapperGender(Gender command)
        {
            var genderVm = new GenderVm
            {
                Id = command.Id,
                Type = command.Type,
            };

            return genderVm;
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
