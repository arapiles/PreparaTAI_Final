using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using WebApiProyecto.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS antes que otros servicios o no va
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("ReglasCors",
        builder => builder
        .WithOrigins("http://localhost:5173", "https://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Otros servicios
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApitaiContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddControllers().AddJsonOptions(options =>
{ //errores de serializacion
    //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // quita claves que usa swawgger para controlar
    options.JsonSerializerOptions.MaxDepth = 32;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = false;  // Para evitar esos metadatos
});

var app = builder.Build();

//Aplicar CORS lo antes posible
app.UseCors("ReglasCors");

// Configuración de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewwawre orden correcto
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
