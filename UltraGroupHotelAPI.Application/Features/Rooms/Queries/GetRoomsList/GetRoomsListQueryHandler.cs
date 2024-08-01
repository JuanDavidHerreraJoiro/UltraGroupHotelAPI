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
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList
{
    public class GetRoomsListQueryHandler : IRequestHandler<GetRoomsListQuery, List<RoomVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomsListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoomVm>> Handle(GetRoomsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Room>().GetAsync(
                a => a.Hotel.IsEnabled == true,
                null,
                new List<Expression<Func<Room, object>>>{ a => a.Hotel}
            );

            List<RoomVm> roomsVmList = new List<RoomVm>();

            foreach (var item in list)
            {
                roomsVmList.Add(await Mapper(item));
            }

            return roomsVmList;
        }

        private async Task<RoomVm> Mapper(Room command)
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

            return roomVm;
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
