using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList
{
    public class GetRoomTypesListQueryHandler : IRequestHandler<GetRoomTypesListQuery, List<RoomTypeVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomTypesListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoomTypeVm>> Handle(GetRoomTypesListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<RoomType>().GetAllAsync();

            List<RoomTypeVm> roomTypesVmList = new List<RoomTypeVm>();

            foreach (var user in list)
            {
                roomTypesVmList.Add(Mapper(user));
            }

            return roomTypesVmList;
        }

        private RoomTypeVm Mapper(RoomType command)
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
