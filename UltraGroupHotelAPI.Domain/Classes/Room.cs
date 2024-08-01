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
    [Table("tbl_Rooms")]
    public class Room : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string RoomId { get; set; } = string.Empty;
        public double Cost { get; set; }
        public double BaseAmount { get; set; }
        public double Tax { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
    }
}
