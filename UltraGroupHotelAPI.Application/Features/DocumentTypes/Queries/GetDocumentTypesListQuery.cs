using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries
{
    public class GetDocumentTypesListQuery : IRequest<List<DocumentTypeVm>>
    {
    }
}
