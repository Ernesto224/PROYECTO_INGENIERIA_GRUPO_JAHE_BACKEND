using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class EstadoHabitacionRepositorio : IEstadoHabitacionRepositorio
    {


        private readonly ContextoDbSQLServer _contexto;

        public EstadoHabitacionRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<object> ActualizarEstadoDeHabitacion(int idHabitacion, string nuevoEstado)
        {
            try
            {
                var habitacion = await this._contexto.Habitaciones
                    .FirstOrDefaultAsync(h => h.IdHabitacion == idHabitacion);

                if (habitacion == null)
                {
                    return new { Exitoso = false, Mensaje = $"No se encontró la habitación con ID {idHabitacion}." };
                }

                var estadoActual = habitacion.Estado.ToUpper();
                var nuevoEstadoUpper = nuevoEstado.ToUpper();

                // Validación de estados no permitidos
                if ((estadoActual == "RESERVADA" || estadoActual == "OCUPADA") &&
                    (nuevoEstadoUpper == "DISPONIBLE" || nuevoEstadoUpper == "NO_DISP"))
                {
                    return new
                    {
                        Exitoso = false,
                        Mensaje = $"La habitación se encuentra en el estado '{estadoActual}'. No puedes cambiarla a '{nuevoEstadoUpper}'."
                    };
                }

                habitacion.Estado = nuevoEstado;
                await this._contexto.SaveChangesAsync();

                return new
                {
                    Exitoso = true,
                    Mensaje = "Estado actualizado correctamente.",
                    Habitacion = habitacion
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Exitoso = false,
                    Mensaje = $"Error al actualizar el estado de la habitación: {ex.Message}"
                };
            }
        }



        public async Task<IEnumerable<Habitacion>> verHabitaciones()
        {
            try
            {
                var habitaciones = await this._contexto.Habitaciones
                    .Include(h => h.TipoDeHabitacion)
                    .ThenInclude(th => th.Imagen)
                    .ToListAsync();

                return habitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las habitaciones: {ex.Message}");
            }
        }
    }
}
