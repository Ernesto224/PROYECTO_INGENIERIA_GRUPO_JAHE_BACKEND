using Aplicacion.DTOs;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class TarifasRepositorio : ITarifasRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;
    

        public TarifasRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

      


        public async Task<IEnumerable<TipoDeHabitacion>> verTarifas()
        {
            try
            {
                var TipoDeHabitaciones = await this._contexto.tipoDeHabitacion
                    .Include(th => th.Imagen) // Incluye los datos de la imagen
                    .ToListAsync();

                if (TipoDeHabitaciones == null || !TipoDeHabitaciones.Any())
                    throw new Exception("No se encontraron datos.");


                return TipoDeHabitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<object> ActualizarTipoDeHabitacion(TipoDeHabitacion tipoDeHabiatcionActualizado, string? urlImagen)
        {
            try
            {
                // Buscar la habitación que se desea actualizar
                var tipoDeHabitacion = await this._contexto.tipoDeHabitacion
                    .Include(th => th.Imagen)  // Incluir la entidad Imagen asociada
                    .FirstOrDefaultAsync(th => th.IdTipoDeHabitacion == tipoDeHabiatcionActualizado.IdTipoDeHabitacion);

                if (tipoDeHabitacion == null)
                    throw new Exception("Tipo de habitación no encontrado.");

                // Actualizar los datos de la habitación
                tipoDeHabitacion.Nombre = tipoDeHabiatcionActualizado.Nombre;
                tipoDeHabitacion.Descripcion = tipoDeHabiatcionActualizado.Descripcion;
                tipoDeHabitacion.TarifaDiaria = tipoDeHabiatcionActualizado.TarifaDiaria;

                // Si se ha proporcionado una nueva URL de imagen, se actualiza
                if (urlImagen != null)
                {
                    // Actualizamos la URL de la imagen
                    tipoDeHabitacion.Imagen.Url = urlImagen;
                }

                // Guardamos los cambios en la base de datos
                await this._contexto.SaveChangesAsync();

                return new
                {
                    tipoDeHabitacion.IdTipoDeHabitacion,
                    tipoDeHabitacion.Nombre,
                    tipoDeHabitacion.Descripcion,
                    tipoDeHabitacion.TarifaDiaria,
                    ImagenUrl = tipoDeHabitacion.Imagen.Url
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el tipo de habitación: {ex.Message}");
            }
        }

        
    }
}
