using GestionHotelApi.Data;
using GestionHotelApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez des services à la container.
builder.Services.AddControllers();

// Ajoutez la configuration de MongoDB

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<ChambreService>();
builder.Services.AddSingleton<UtilisateurService>();
builder.Services.AddSingleton<ReservationService>();
// Ajoutez la configuration de Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure le pipeline de requêtes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
