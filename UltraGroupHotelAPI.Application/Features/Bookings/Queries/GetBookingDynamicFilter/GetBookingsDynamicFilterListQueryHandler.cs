using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomAvailableList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookingDynamicFilter
{
    public class GetBookingsDynamicFilterListQueryHandler : IRequestHandler<GetBookingsDynamicFilterListQuery, BookingDynamicFilterVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBookingsDynamicFilterListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookingDynamicFilterVm> Handle(GetBookingsDynamicFilterListQuery request, CancellationToken cancellationToken)
        {
            /*var filterCondition = (
                (request.EntryDate ?? DateTime.MinValue) == a.Booking.EntryDate ||
                (request.ExitDate ?? DateTime.MaxValue) == a.Booking.ExitDate ||
                (request.Capacity > 0 && a.Capacity >= request.Capacity) ||
                (request.CityId ?? 0) == a.Hotel.CityId
            );*/

            var list = await _unitOfWork.Repository<Room>().GetAsync(
                a =>
                (
                    (request.EntryDate ?? DateTime.MinValue) == a.Booking.EntryDate ||
                    (request.ExitDate ?? DateTime.MaxValue) == a.Booking.ExitDate ||
                    (request.Capacity > 0 && a.Capacity >= request.Capacity) ||
                    (request.CityId.HasValue ? a.Hotel.CityId == request.CityId : true)
                ) &&
                (a.Hotel.IsEnabled == true && a.IsEnabled == true && a.IsAvailable == true),
                null,
                new List<Expression<Func<Room, object>>> { a => a.Hotel, a => a.Booking }
            );

            //var listFilter = list.Where(a=> a.Hotel.IsEnabled == true && a.IsEnabled == true && a.IsAvailable == true).ToList();

            BookingDynamicFilterVm bookingDynamicFilterVmList = new BookingDynamicFilterVm();

            bookingDynamicFilterVmList.Rooms = await ListRoomVm(list);

            return bookingDynamicFilterVmList;
        }

        private async Task<List<RoomVm>> ListRoomVm(IEnumerable<Room> rooms)
        {
            //var listcommand = await _unitOfWork.Repository<Room>().GetAsync(a => a.BookingId == BookingId);
            List<RoomVm> roomVmList = new List<RoomVm>();

            foreach (var command in rooms)
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
    }
}
