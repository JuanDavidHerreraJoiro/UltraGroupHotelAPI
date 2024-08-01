using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Constants;
using UltraGroupHotelAPI.Application.Contracts.Identity;
using UltraGroupHotelAPI.Application.Models.Identity;
using UltraGroupHotelAPI.Domain.Enum;
using UltraGroupHotelAPI.Identity.Models;

namespace UltraGroupHotelAPI.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.User);
            //var user = await _userManager.Users.FirstOrDefault(a=>a.UserName==request.User);
            if (user == null)
            {
                throw new Exception($"El usuario con el email {request.User} no existe");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password,false,lockoutOnFailure:false);

            if (!result.Succeeded)
            {
                throw new Exception($"Las credenciales son incorrectas");
            }

            var token = await GenerateToken(user);

            var authResponse = new AuthResponse 
            { 
                Id = user.Id,
                User = user.UserName,
                TokenJwt = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return authResponse;
        }

        public async Task<Response> Register(RegistrationRequest request, EnumRoles enumRoles)
        {
            var userExist = await _userManager.FindByNameAsync(request.UserName);
            List<string> errors = new List<string>();

            if (userExist != null)
            {
                errors.Add("El usuario ya existe");
                return new Response(409, "El usuario ya existe", errors);
            }

            var hasher = new PasswordHasher<ApplicationUser>();

            var newUser = new ApplicationUser
            {
                Email = request.Email,
                NormalizedEmail = request.Email,
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper() ,
                PasswordHash = hasher.HashPassword(null, request.Password),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser,request.Password);

            if (result.Succeeded)
            {
                //"User"
                await _userManager.AddToRoleAsync(newUser, enumRoles.ToString());

                var token = await GenerateToken(newUser);


                return new Response(
                    new RegistrationResponse
                    {
                        Id = newUser.Id,
                        UserName = newUser.UserName,
                        TokenJwt = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                );
 
            }

            foreach ( var item in result.Errors)
            {
                errors.Add(item.Description);
            }

            return new Response(500, "Error interno del servidor", null, errors);

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();

            foreach ( var role in roles)
            {
                rolesClaims.Add(new Claim(ClaimTypes.Role,role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(rolesClaims);

            var symmetricSecuriryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecuriryKey,SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires:DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
    }
}
