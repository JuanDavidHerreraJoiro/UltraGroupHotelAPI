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
    [Table("tbl_RoomTypes")]
    public class RoomType : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
