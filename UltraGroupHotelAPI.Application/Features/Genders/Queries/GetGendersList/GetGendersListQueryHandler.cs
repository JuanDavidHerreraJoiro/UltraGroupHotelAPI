using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList
{
    public class GetGendersListQueryHandler : IRequestHandler<GetGendersListQuery, List<GenderVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetGendersListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GenderVm>> Handle(GetGendersListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Gender>().GetAllAsync();

            List<GenderVm> gendersVmList = new List<GenderVm>();

            foreach (var user in list)
            {

                gendersVmList.Add(Mapper(user));
            }

            //return _mapper.Map<List<PersonesVm>>(userList);
            return gendersVmList;
        }

        private GenderVm Mapper(Gender command)
        {
            var gendersVm = new GenderVm
            {
                Id = command.Id,
                Type = command.Type,
            };

            return gendersVm;
        }
    }
}
