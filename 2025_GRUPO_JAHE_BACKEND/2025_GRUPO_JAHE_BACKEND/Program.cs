using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DBSomee.com");

// Configurar Entity Framework Core con SQL Server
builder.Services.AddDbContext<ContextoDbSQLServer>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IHomeRepositorio, HomeRepositorio>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
