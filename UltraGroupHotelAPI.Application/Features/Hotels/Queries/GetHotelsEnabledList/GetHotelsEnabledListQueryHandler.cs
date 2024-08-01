using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList
{
    public class GetHotelsEnabledListQueryHandler : IRequestHandler<GetHotelsEnabledListQuery, List<HotelEnabledVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetHotelsEnabledListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<HotelEnabledVm>> Handle(GetHotelsEnabledListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Hotel>().GetAsync(a => a.IsEnabled == true);

            List<HotelEnabledVm> hotelsVmList = new List<HotelEnabledVm>();

            foreach (var item in list)
            {
                hotelsVmList.Add(await Mapper(item));
            }

            return hotelsVmList;
        }
        private async Task<HotelEnabledVm> Mapper(Hotel command)
        {
            var hotelEnabledVm = new HotelEnabledVm
            {
                Id = command.Id,
                HotelName = command.HotelName,
                IsEnabled = command.IsEnabled,
                City = MapperCity(await _unitOfWork.Repository<City>().GetByTypeAsync(a => a.Id == command.CityId)),
                Rooms = await ListRoomVm(command.Id),
                UserId = command.UserId
            };

            return hotelEnabledVm;
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
