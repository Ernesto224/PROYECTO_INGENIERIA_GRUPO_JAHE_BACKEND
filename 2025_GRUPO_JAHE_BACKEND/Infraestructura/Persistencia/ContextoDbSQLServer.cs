using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia
{
    public class ContextoDbSQLServer : DbContext
    {
        public ContextoDbSQLServer(DbContextOptions<ContextoDbSQLServer> options) : base(options)
        {
        }

        // Definir DbSets para las entidades
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<SobreNosotros> SobreNosotros { get; set; }
        public DbSet<Facilidad> Facilidades { get; set; }
        public DbSet<Publicidad> Publicidades { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<TipoDeHabitacion> TipoDeHabitaciones { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones personalizadas si es necesario
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDbSQLServer).Assembly);
            modelBuilder.Entity<Imagen_SobreNosotros>()
            .HasKey(isn => new { isn.IdImagen, isn.IdSobreNosotros });
        }
    }
}
