using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Common;

namespace UltraGroupHotelAPI.Domain.Classes
{
    [Table("tbl_Bookings")]
    public class Booking : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int NumberPeople { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public int EmergencyContactId { get; set; }
        public EmergencyContact? EmergencyContact { get; set; }
        public int TravelerId { get; set; }
        public Traveler? Traveler { get; set; }
        public List<Room>? Rooms { get; set; }
    }
}
