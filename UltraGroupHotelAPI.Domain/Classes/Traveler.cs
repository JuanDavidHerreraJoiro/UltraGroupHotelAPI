using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Common;
using UltraGroupHotelAPI.Domain.Enum;

namespace UltraGroupHotelAPI.Domain.Classes
{
    [Table("tbl_Travelers")]
    public class Traveler : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public List<Booking>? Bookings { get; set; }
        public string? UserId { get; set; } = string.Empty;
    }
}
