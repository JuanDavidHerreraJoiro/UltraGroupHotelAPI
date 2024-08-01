
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Enum;
using UltraGroupHotelAPI.Identity.Models;

namespace UltraGroupHotelAPI.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "d071b9dd-d5fd-410c-b97f-679a1bb2864b",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com",
                    UserName = EnumRoles.Administrator.ToString(),
                    NormalizedUserName = EnumRoles.Administrator.ToString().ToUpper(),
                    PasswordHash = hasher.HashPassword(null, EnumRoles.Administrator.ToString()),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "fe503470-37e4-4951-8ba0-12c74d409be1",
                    Email = "user@gmail.com",
                    NormalizedEmail = "user@gmail.com",
                    UserName = EnumRoles.Traveler.ToString(),
                    NormalizedUserName = EnumRoles.Traveler.ToString().ToUpper(),
                    PasswordHash = hasher.HashPassword(null, EnumRoles.Traveler.ToString()),
                    EmailConfirmed = true,
                }
            ) ;
        }
    }
}
