using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Infrastructure.Persistence;

namespace UltraGroupHotelAPI.Infrastructure.Seeds
{
    public class SeedDocumentTypes
    {
        public static async Task SeedAsync(UltraGroupHotelDbContext context, ILogger<SeedDocumentTypes> logger)
        {
            try
            {
                if (!context.DocumentTypes.Any())
                {
                    context.DocumentTypes.AddRange(GetPreconfiguredGender());
                    await context.SaveChangesAsync();
                    logger.LogInformation("Data seed new for tha tabledocument Type of database {context}", typeof(UltraGroupHotelDbContext).Name);
                }
            }
            catch (SqlException)
            {

                throw;
            }
        }

        private static IEnumerable<DocumentType> GetPreconfiguredGender()
        {
            return new List<DocumentType>
            {
                new DocumentType { Type = "DNI (Documento Nacional de Identidad)" },
                new DocumentType { Type = "Pasaporte" },
                new DocumentType { Type = "Carnet de conducir" },
                new DocumentType { Type = "Tarjeta de Residencia" },
                new DocumentType { Type = "Tarjeta de Identidad" },
                new DocumentType { Type = "RUT (Rol Unico Tributario)" },
                new DocumentType { Type = "NIE (Numero de Identidad de Extrangero)" },
                new DocumentType { Type = "CURP (Clave Unica de Registro de Población)" },
                new DocumentType { Type = "NIT (Numero de Identificacion fiscal)" },
                new DocumentType { Type = "Registro Civil" },
                new DocumentType { Type = "Certificado de Nacimiento" },
                new DocumentType { Type = "Otros" }
            };
        }
    }
}
