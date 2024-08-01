
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Identity.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4a9e53ab-6a1c-460b-9509-bb2a10dd09c9",
                    UserId = "d071b9dd-d5fd-410c-b97f-679a1bb2864b"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "d92c40f5-c93b-4847-9104-eaa47af91a91",
                    UserId = "fe503470-37e4-4951-8ba0-12c74d409be1"
                }
            );
        }
    }
}
