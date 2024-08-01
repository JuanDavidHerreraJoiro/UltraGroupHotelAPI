using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelRoomsEnabledList
{
    public class GetHotelRoomsEnabledListQueryHandler : IRequestHandler<GetHotelRoomsEnabledListQuery, HotelRoomsEnabledVm>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetHotelRoomsEnabledListQueryHandler> _logger;
        public GetHotelRoomsEnabledListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<HotelRoomsEnabledVm> Handle(GetHotelRoomsEnabledListQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _unitOfWork.Repository<Hotel>().GetByTypeAsync(a => a.Id == request.HotelId && a.IsEnabled == true);

            if (hotel == null )
            {
                return null;
            }

            HotelRoomsEnabledVm hotelRoomsEnabledVm = new HotelRoomsEnabledVm();

            hotelRoomsEnabledVm.HotelName = hotel.HotelName;
            hotelRoomsEnabledVm.IsEnabled = hotel.IsEnabled;
            hotelRoomsEnabledVm.City = MapperCity(await _unitOfWork.Repository<City>().GetByTypeAsync(a => a.Id == hotel.CityId));
            hotelRoomsEnabledVm.Rooms = await ListRoomVm(hotel.Id);

            return hotelRoomsEnabledVm;
        }

        private async Task<List<RoomVm>> ListRoomVm(int hotelId)
        {
            var listcommand = await _unitOfWork.Repository<Room>().GetAsync(a => a.HotelId == hotelId && a.IsAvailable == true);
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
                    RoomType = MapperRoomType(await _unitOfWork.Repository<RoomType>().GetByTypeAsync(a => a.Id == command.RoomTypeId))
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

        private CityVm MapperCity(City command)
        {
            var roomTypeVm = new CityVm
            {
                Id = command.Id,
                CityName = command.CityName,
            };

            return roomTypeVm;
        }
    }
}
