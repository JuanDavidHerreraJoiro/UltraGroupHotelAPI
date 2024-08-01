using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;
using Microsoft.Extensions.Logging;
using UltraGroupHotelAPI.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;

namespace UltraGroupHotelAPI.Infrastructure.Seeds
{

    public class SeedRoomTypes 
    {
        public static async Task SeedAsync(UltraGroupHotelDbContext context, ILogger<SeedRoomTypes> logger)
        {
            try
            {
                if (!context.RoomTypes.Any())
                {
                    context.RoomTypes.AddRange(GetPreconfiguredGender());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Data seed new for tha table roomtype of database {context}", typeof(UltraGroupHotelDbContext).Name);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        private static IEnumerable<RoomType> GetPreconfiguredGender()
        {
            return new List<RoomType>
            {
                new RoomType { Type = "Habitación Individual", Description = "Diseñada para una persona. Generalmente tiene una cama individual o una cama doble." },
                new RoomType { Type = "Habitación Doble", Description = "Diseñada para dos personas. Puede tener una cama doble o dos camas individuales." },
                new RoomType { Type = "Habitación Twin", Description = "Diseñada para dos personas. Tiene dos camas individuales." },
                new RoomType { Type = "Habitación Triple", Description = "Diseñada para tres personas. Puede tener tres camas individuales, una cama doble y una cama individual, o una combinación similar." },
                new RoomType { Type = "Habitación Cuádruple", Description = "Diseñada para cuatro personas. Puede tener dos camas dobles o una combinación de camas dobles y individuales." },
                new RoomType { Type = "Suite", Description = "Habitaciones más grandes y lujosas. Generalmente incluyen un área de estar y dormitorio separados, y pueden incluir servicios adicionales como cocina o jacuzzi." },
                new RoomType { Type = "Junior Suite", Description = "Más pequeña que una suite estándar. Puede incluir un área de estar pequeña además del dormitorio." },
                new RoomType { Type = "Suite Presidencial", Description = "La habitación más lujosa y grande de un hotel. Ofrece las mejores vistas, múltiples habitaciones, servicios de alta calidad y amenities exclusivos." },
                new RoomType { Type = "Habitación Familiar", Description = "Diseñada para familias. Puede incluir camas adicionales o sofás cama." },
                new RoomType { Type = "Habitación Deluxe", Description = "Más grande y con mejores servicios que una habitación estándar. Puede incluir una cama king-size, mejores vistas y servicios adicionales." },
                new RoomType { Type = "Habitación Ejecutiva", Description = "Diseñada para viajeros de negocios. Puede incluir un espacio de trabajo, acceso a salas de reuniones y otros servicios de negocios." },
                new RoomType { Type = "Habitación de Lujo", Description = "Habitaciones de alta calidad con servicios premium. Diseñadas para proporcionar una experiencia lujosa." },
                new RoomType { Type = "Habitación con Vista", Description = "Habitaciones que ofrecen vistas panorámicas, como vistas al mar, vistas a la ciudad, vistas a la montaña, etc." },
            };
        }
    }
}
