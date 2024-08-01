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

namespace UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries
{
    public class GetDocumentTypesListQueryHandler : IRequestHandler<GetDocumentTypesListQuery, List<DocumentTypeVm>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDocumentTypesListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DocumentTypeVm>> Handle(GetDocumentTypesListQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<DocumentType>().GetAllAsync();

            List<DocumentTypeVm> documentTypesVmList = new List<DocumentTypeVm>();

            foreach (var user in list)
            {
                documentTypesVmList.Add(Mapper(user));
            }

            //return _mapper.Map<List<PersonesVm>>(userList);
            return documentTypesVmList;
        }

        private DocumentTypeVm Mapper(DocumentType command)
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
