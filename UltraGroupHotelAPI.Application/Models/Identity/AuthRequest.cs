using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Models.Identity
{
    public class AuthRequest
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
