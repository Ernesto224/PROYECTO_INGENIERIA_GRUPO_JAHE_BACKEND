using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Infraestructura.Repositorios;
using Infraestructura.ServiciosExternos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

// Obtener la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DBSomee.com");

// Configurar Entity Framework Core con SQL Server
builder.Services.AddDbContext<ContextoDbSQLServer>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IHomeRepositorio, HomeRepositorio>();
builder.Services.AddScoped<IHomeServicio, HomeServicio>();
builder.Services.AddScoped<IFacilidadRepositorio, FacilidadRepositorio>();
builder.Services.AddScoped<IFacilidadServicio, FacilidadServicio>();
builder.Services.AddScoped<ITarifasRepositorio, TarifasRepositorio>();
builder.Services.AddScoped<ITarifasServicio, TarifasServicio>();

builder.Services.AddScoped<IPublicidadRepositorio, PublicidadRepositorio>();
builder.Services.AddScoped<IPublicidadServicio, PublicidadServicio>();
builder.Services.AddScoped<ISobreNosotrosServicio, SobreNosotrosServicio>();
builder.Services.AddScoped<ISobreNosotrosRepositorio, SobreNosotrosRepositorio>();
builder.Services.AddScoped<IContactoRepositorio, ContactoRepositorio>();
builder.Services.AddScoped<IContactoServicio, ContactoServicio>();
builder.Services.AddScoped<IDireccionRepositorio, DireccionRepositorio>();
builder.Services.AddScoped<IDireccionServicio, DireccionServicio>();
// Se optiene la URL de Cloudinary del archivo appsettings.json
var cloudinaryUrl = builder.Configuration.GetSection("Cloudinary").GetSection("Url").Value;
// Se agrega el servicio de almacenamiento de imagenes a la inyección de dependencias
builder.Services.AddScoped<IServicioAlmacenamientoImagenes>(servicioImagenes => new CloudinaryAlmacenamientoImagen(cloudinaryUrl!));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//configuracion de cores
app.UseCors("AllowOrigin");
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

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
