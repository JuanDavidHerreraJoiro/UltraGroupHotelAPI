using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Domain.Common;
using UltraGroupHotelAPI.Infrastructure.Configuration;
using UltraGroupHotelAPI.Infrastructure.Seeds;

namespace UltraGroupHotelAPI.Infrastructure.Persistence
{
    public class UltraGroupHotelDbContext : DbContext
    {
        public UltraGroupHotelDbContext(DbContextOptions<UltraGroupHotelDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Traveler> Travelers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new RoomConfiguration().Configure(modelBuilder.Entity<Room>());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.Now;
                        entry.Entity.CreationBy = "SYSTEM";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateBy = "SYSTEM";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
