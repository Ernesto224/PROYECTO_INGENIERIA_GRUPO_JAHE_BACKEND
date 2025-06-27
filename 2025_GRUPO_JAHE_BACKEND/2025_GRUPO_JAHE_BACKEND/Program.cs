using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Dominio.Interfaces;
using Dominio.Interfaces.Seguridad;
using Infraestructura.Persistencia;
using Infraestructura.Repositorios;
using Infraestructura.Seguridad;
using Infraestructura.ServiciosExternos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Obtener la cadena de conexi�n del archivo appsettings.json
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
builder.Services.AddScoped<IEstadoHabitacionRepositorio, EstadoHabitacionRepositorio>();
builder.Services.AddScoped<IEstadoHabitacionServicio, EstadoHabitacionServicio>();
builder.Services.AddScoped<IPublicidadRepositorio, PublicidadRepositorio>();
builder.Services.AddScoped<IPublicidadServicio, PublicidadServicio>();
builder.Services.AddScoped<ISobreNosotrosServicio, SobreNosotrosServicio>();
builder.Services.AddScoped<ISobreNosotrosRepositorio, SobreNosotrosRepositorio>();
builder.Services.AddScoped<IContactoRepositorio, ContactoRepositorio>();
builder.Services.AddScoped<IContactoServicio, ContactoServicio>();
builder.Services.AddScoped<IDireccionRepositorio, DireccionRepositorio>();
builder.Services.AddScoped<IDireccionServicio, DireccionServicio>();
builder.Services.AddScoped<IReservaServicio, ReservaServicio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<ITransactionMethods, TransactionMethods>();
builder.Services.AddScoped<IHabitacionRepositorio, HabitacionRepositorio>();
builder.Services.AddScoped<IHabitacionServicio, HabitacionServicio>();
builder.Services.AddScoped<IAdministradorRepositorio, AdministradorRepositorio>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();
builder.Services.AddScoped<IOfertaRepositorio, OfertaRepositorio>();
builder.Services.AddScoped<IOfertaServicio, OfertaServicio>();
builder.Services.AddScoped<ITemporadaRepositorio, TemporadaRepositorio>();
builder.Services.AddScoped<ITemporadaServicio, TemporadaServicio>();



// Se optiene la URL de Cloudinary del archivo appsettings.json
var cloudinaryUrl = builder.Configuration.GetSection("Cloudinary").GetSection("Url").Value;
// Se agrega el servicio de almacenamiento de imagenes a la inyecci�n de dependencias
builder.Services.AddScoped<IServicioAlmacenamientoImagenes>(servicioImagenes => new CloudinaryAlmacenamientoImagen(cloudinaryUrl!));

// Servicio de generacion de tokens resibe la configuracion del servicio
builder.Services.AddScoped<IServicioGeneracionDeTokens>(servicioTokens => new ServicioGeneracionDeTokens(builder.Configuration));

// Configuraciones del JWTBearer
builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JwtSecretKey:key"]!))
    };
});
var servicioEmailUrl = builder.Configuration.GetSection("EmailServiceApp").GetSection("Url").Value;
builder.Services.AddScoped<IServicioEmail>(servicioEmail => new ServicioEmail(servicioEmailUrl!, new HttpClient()));


builder.Services.AddControllers();
builder.Services.AddHttpClient();
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
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
