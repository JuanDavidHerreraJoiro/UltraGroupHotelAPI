using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Identity.Configuration;
using UltraGroupHotelAPI.Identity.Models;

namespace UltraGroupHotelAPI.Identity
{
    public class UltraGroupHotelIdentityDbContext :IdentityDbContext<ApplicationUser>
    {
        public UltraGroupHotelIdentityDbContext(DbContextOptions<UltraGroupHotelIdentityDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
