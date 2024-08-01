using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries;

namespace UltraGroupHotelAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDocumentTypes")]
        [ProducesResponseType(typeof(IEnumerable<DocumentTypeVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DocumentTypeVm>>> GetDocumentTypes()
        {
            var query = new GetDocumentTypesListQuery();

            var list = await _mediator.Send(query);

            return Ok(list);
        }
    }
}
