using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Models.Identity
{
    public class RegistrationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string TokenJwt { get; set; }        
    }
}
