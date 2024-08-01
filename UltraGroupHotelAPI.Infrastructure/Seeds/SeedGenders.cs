using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Infrastructure.Persistence;

namespace UltraGroupHotelAPI.Infrastructure.Seeds
{
    public class SeedGenders
    {
        public static async Task SeedAsync(UltraGroupHotelDbContext context, ILogger<SeedGenders> logger)
        {
            try
            {
                if (!context.Genders.Any())
                {
                    context.Genders.AddRange(GetPreconfiguredGender());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Data seed new for tha table genders of database {context}", typeof(UltraGroupHotelDbContext).Name);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        private static IEnumerable<Gender> GetPreconfiguredGender()
        {
            return new List<Gender>
            {
                new Gender { Type = "Hombre" },
                new Gender { Type = "Mujeres" },
                new Gender { Type = "Transgénero" },
                new Gender { Type = "No Binario" },
                new Gender { Type = "Género Fluido" },
                new Gender { Type = "Otro" }
            };
        }
    }
}
