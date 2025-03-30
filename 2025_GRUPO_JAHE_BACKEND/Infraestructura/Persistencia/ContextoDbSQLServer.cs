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

        public DbSet<Publicidad> Publicidades { get; set; }

        public DbSet<Oferta> Ofertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplicar configuraciones personalizadas si es necesario
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextoDbSQLServer).Assembly);
        }
    }
}
