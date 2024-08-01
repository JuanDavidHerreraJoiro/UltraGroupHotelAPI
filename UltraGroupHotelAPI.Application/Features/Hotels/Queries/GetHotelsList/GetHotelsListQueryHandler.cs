using AutoMapper;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList
{
    public class GetHotelsListQueryHandler : IRequestHandler<GetHotelsListQuery, List<HotelVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetHotelsListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HotelVm>> Handle(GetHotelsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Hotel>().GetAllAsync();

            List<HotelVm> hotelsVmList = new List<HotelVm>();

            foreach (var item in list)
            {
                hotelsVmList.Add(await Mapper(item));
            }

            return hotelsVmList;
        }

        private async Task<HotelVm> Mapper(Hotel command)
        {
            var hotelVm = new HotelVm
            {
                Id = command.Id,
                HotelName=command.HotelName,
                IsEnabled = command.IsEnabled,
                City = MapperCity(await _unitOfWork.Repository<City>().GetByTypeAsync(a=>a.Id == command.CityId)),
                Rooms = await ListRoomVm(command.Id),
                UserId = command.UserId
            };

            return hotelVm;
        }

        private async Task<List<RoomVm>> ListRoomVm(int hotelId)
        {
            var listcommand = await _unitOfWork.Repository<Room>().GetAsync(a => a.HotelId == hotelId);
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
