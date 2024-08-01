
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Enum;

namespace UltraGroupHotelAPI.Identity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                
                new IdentityRole
                {
                    Id = "4a9e53ab-6a1c-460b-9509-bb2a10dd09c9",
                    Name = EnumRoles.Administrator.ToString(),
                    NormalizedName = "ADMINISTRATOR",
                },
                new IdentityRole
                {
                    Id = "d92c40f5-c93b-4847-9104-eaa47af91a91",
                    Name = EnumRoles.Traveler.ToString(),
                    NormalizedName = "TRAVELER",
                });
        }
    }
}
