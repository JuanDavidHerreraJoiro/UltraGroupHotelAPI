using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries
{
    public class DocumentTypeVm
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
