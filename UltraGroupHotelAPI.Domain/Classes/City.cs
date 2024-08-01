using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Common;

namespace UltraGroupHotelAPI.Domain.Classes
{
    [Table("tbl_Cities")]
    public class City : EntityBase
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
