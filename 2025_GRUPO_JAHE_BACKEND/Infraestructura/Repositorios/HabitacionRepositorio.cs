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
    }
}
