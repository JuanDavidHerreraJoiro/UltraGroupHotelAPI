using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Infrastructure.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            builder.HasKey(r => r.Id); 
            
            builder.Property(r => r.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
