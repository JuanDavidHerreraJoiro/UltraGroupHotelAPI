using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Models.Identity;
using UltraGroupHotelAPI.Domain.Enum;

namespace UltraGroupHotelAPI.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request );

        Task<Response> Register(RegistrationRequest request,EnumRoles enumRoles);

    }
}
