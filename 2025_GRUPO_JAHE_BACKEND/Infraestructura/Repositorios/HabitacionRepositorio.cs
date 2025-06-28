using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infraestructura.Repositorios
{
    public class HabitacionRepositorio : IHabitacionRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public HabitacionRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<(IEnumerable<Habitacion> habitaciones, int datosTotales, int paginaActual)> ConsultarDisponibilidadDeHabitaciones(int[] idTiposHabitacion, 
            DateTime fechaLlegada, DateTime fechaSalida, int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            try 
            {
                // Se construye la consulta base de habitaciones disponibles
                var query = _contexto.Habitaciones
                    .Include(h => h.TipoDeHabitacion) // Se incluye la relación con el tipo de habitación
                    .Where(h => idTiposHabitacion.Contains(h.TipoDeHabitacion.IdTipoDeHabitacion)) // Filtro por tipo de habitación
                    .Where(h => !this._contexto.Reservas
                        .Where(r => r.FechaLlegada < fechaSalida && r.FechaSalida > fechaLlegada) // Se filtran reservas que se cruzan con el rango deseado
                        .Select(r => r.IdHabitacion) // Se seleccionan los IDs de habitaciones reservadas
                        .Contains(h.IdHabitacion)); // Se excluyen las habitaciones que están reservadas

                // Se cuenta el total de habitaciones disponibles con los filtros aplicados
                int totalRegistros = await query.CountAsync();

                // Se calcula el total de páginas
                int totalDePaginas = (int)Math.Ceiling((double)totalRegistros / maximoDeDatos);

                // Se determina la página actual según si se solicita ir a la última página o no
                int paginaActual = irALaUltimaPagina ? (totalDePaginas == 0 ? 1 : totalDePaginas) : numeroDePagina;

                // Se aplica la paginación sobre la consulta ya construida
                var resultados = await query
                    .Skip((paginaActual - 1) * maximoDeDatos) // Salta los registros de páginas anteriores
                    .Take(maximoDeDatos) // Toma el número máximo definido de datos
                    .ToListAsync(); // Ejecuta la consulta

                // Se retornan los resultados junto con el total de datos encontrados
                return (resultados, totalRegistros, paginaActual);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(IEnumerable<Habitacion> habitaciones, int datosTotales, int paginaActual)> ConsultarDisponibilidadDeHabitacionesHoy(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            try
            {
                DateTime fechaActual = DateTime.Today;

                // Consulta base con estado personalizado
                var queryBase = _contexto.Habitaciones
                    .Include(h => h.TipoDeHabitacion)
                    .AsQueryable();

                // Proyección con el estado personalizado
                var queryConEstado = queryBase.Select(h => new
                {
                    Habitacion = h,
                    EstadoPersonalizado =
                (h.Estado == "OCUPADA" || h.Estado == "NO_DISP") ? h.Estado :
                _contexto.Reservas.Any(r =>
                    r.IdHabitacion == h.IdHabitacion &&
                    fechaActual >= r.FechaLlegada &&
                    fechaActual < r.FechaSalida) ? "RESERVADA" : "DISPONIBLE"
                });


                // Se cuenta el total de habitaciones disponibles con los filtros aplicados
                int totalRegistros = await queryConEstado.CountAsync();

                // Se calcula el total de páginas
                int totalDePaginas = (int)Math.Ceiling((double)totalRegistros / maximoDeDatos);

                // Se determina la página actual según si se solicita ir a la última página o no
                int paginaActual = irALaUltimaPagina ? (totalDePaginas == 0 ? 1 : totalDePaginas) : numeroDePagina;

                // Se aplica la paginación sobre la consulta ya construida
                var resultados = await queryConEstado
                    .Skip((paginaActual - 1) * maximoDeDatos) // Salta los registros de páginas anteriores
                    .Take(maximoDeDatos) // Toma el número máximo definido de datos
                    .ToListAsync(); // Ejecuta la consulta

                // Asignar el estado personalizado a las habitaciones
                foreach (var item in resultados)
                {
                    item.Habitacion.Estado = item.EstadoPersonalizado;

                }

                // Extraer solo las habitaciones
                var habitaciones = resultados.Select(r => r.Habitacion).ToList();

                // Se retornan los resultados junto con el total de datos encontrados
                return (habitaciones, totalRegistros, paginaActual);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarHabitacionesOcupadasConTimeout()
        {
            var limiteTiempo = DateTime.Now.AddMinutes(-15);

            var habitacionesPorActualizar = _contexto.Habitaciones
                .Where(h => h.Estado == Dominio.Enumeraciones.EstadoDeHabitacion.OCUPADA.ToString()
                         && h.FechaEstado < limiteTiempo);

            foreach (var habitacion in habitacionesPorActualizar)
            {
                habitacion.Estado = Dominio.Enumeraciones.EstadoDeHabitacion.DISPONIBLE.ToString();
                habitacion.FechaEstado = DateTime.Now;
            }

            this._contexto.SaveChangesAsync();

        }
    }
}
