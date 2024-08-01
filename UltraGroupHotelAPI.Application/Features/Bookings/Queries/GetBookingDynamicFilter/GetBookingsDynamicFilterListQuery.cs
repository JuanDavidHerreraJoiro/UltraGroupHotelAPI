using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookingDynamicFilter
{
    public class GetBookingsDynamicFilterListQuery : IRequest<BookingDynamicFilterVm>
    {

        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public int? Capacity { get; set; }
        public int? CityId { get; set; }

        public GetBookingsDynamicFilterListQuery(DateTime? entryDate, DateTime? exitDate, int? capacity, int? cityId)
        {
            EntryDate = entryDate;
            ExitDate = exitDate;
            Capacity = capacity;
            CityId = cityId;
        }
    }
}
