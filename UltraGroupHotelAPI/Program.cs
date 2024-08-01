using Microsoft.OpenApi.Models;
using System.Reflection;
using UltraGroupHotelAPI.API.Middleware;
using UltraGroupHotelAPI.Application;
using UltraGroupHotelAPI.Application.Models.Email;
using UltraGroupHotelAPI.Identity;
using UltraGroupHotelAPI.Infrastructure;
using UltraGroupHotelAPI.Infrastructure.Persistence;
using UltraGroupHotelAPI.Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injection dependency
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAplicationServices();
builder.Services.AddConfigureIdentityServices(builder.Configuration);
builder.Services.Configure<EmailSettings>(options => builder.Configuration.GetSection("EmailSettings").Bind(options));

builder.Services.AddCors(p => p.AddPolicy("CorsUltraGroupPolicy", builder => builder
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
));

var app = builder.Build();

//Seen data
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerGenders = services.GetRequiredService<ILogger<SeedGenders>>();
    var loggerCities = services.GetRequiredService<ILogger<SeedCities>>();
    var loggerRoomTypes = services.GetRequiredService<ILogger<SeedRoomTypes>>();
    var loggerDocumentTypes = services.GetRequiredService<ILogger<SeedDocumentTypes>>();
    var context = services.GetRequiredService<UltraGroupHotelDbContext>();

    await SeedGenders.SeedAsync(context, loggerGenders);
    await SeedCities.SeedAsync(context, loggerCities);
    await SeedRoomTypes.SeedAsync(context, loggerRoomTypes);
    await SeedDocumentTypes.SeedAsync(context, loggerDocumentTypes);
}*/

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsUltraGroupPolicy");

app.MapControllers();

app.Run();
