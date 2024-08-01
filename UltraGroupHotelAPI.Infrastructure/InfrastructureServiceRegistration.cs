using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Infrastructure;
using UltraGroupHotelAPI.Application.Contracts.Persistence;
using UltraGroupHotelAPI.Application.Models.Email;
using UltraGroupHotelAPI.Domain.Classes;
using UltraGroupHotelAPI.Infrastructure.Emails;
using UltraGroupHotelAPI.Infrastructure.Persistence;
using UltraGroupHotelAPI.Infrastructure.Repositories;
using UltraGroupHotelAPI.Infrastructure.Seeds;

namespace UltraGroupHotelAPI.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetEntryAssembly());
            services.AddDbContext<UltraGroupHotelDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CnnUltraGroupHotelAPI"))
                );

            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //RepositoryGenery
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

            //
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            //Services
            services.AddTransient<IEmailServices, EmailService>();

            return services;
        }


    }
}
