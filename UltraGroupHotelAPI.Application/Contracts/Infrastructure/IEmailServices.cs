using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Models.Email;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Contracts.Infrastructure
{
    public interface IEmailServices
    {
        public Task<bool> SendEmailAsync(Email email, Traveler traveler);
    }
}
