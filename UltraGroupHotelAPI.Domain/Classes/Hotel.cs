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
    [Table("tbl_Hotels")]
    public class Hotel : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public List<Room>? Rooms { get; set; }
        public string? UserId { get; set; } = string.Empty;
    }
}
