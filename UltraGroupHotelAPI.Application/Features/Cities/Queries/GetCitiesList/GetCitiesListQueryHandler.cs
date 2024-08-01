using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList
{
    public class GetCitiesListQueryHandler : IRequestHandler<GetCitiesListQuery, List<CityVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCitiesListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CityVm>> Handle(GetCitiesListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<City>().GetAllAsync();

            List<CityVm> citiesVmList = new List<CityVm>();

            foreach (var user in list)
            {
                citiesVmList.Add(await Mapper(user));
            }

            return citiesVmList;
        }

        private async Task<CityVm> Mapper(City command)
        {
            var roomTypeVm = new CityVm
            {
                Id = command.Id,
                CityName = command.CityName
            };

            return roomTypeVm;
        }
    }
}
