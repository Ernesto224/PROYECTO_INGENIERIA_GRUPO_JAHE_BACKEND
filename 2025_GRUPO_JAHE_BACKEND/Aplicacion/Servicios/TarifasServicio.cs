using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class TarifasServicio : ITarifasServicio
    {

        private readonly ITarifasRepositorio _tarifasRepositorio;
        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public TarifasServicio(ITarifasRepositorio tarifasRepositorio, IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            this._tarifasRepositorio = tarifasRepositorio;
            this._servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<IEnumerable<TipoDeHabitacionDTO>> verTarifas()
        {
            var tarifas = await this._tarifasRepositorio.verTarifas();

            if (tarifas == null || !tarifas.Any())
                throw new Exception("No se encontraron datos.");

            return tarifas.Select(tipo => new TipoDeHabitacionDTO
            {
                IdTipoDeHabitacion = tipo.IdTipoDeHabitacion,
                Nombre = tipo.Nombre,
                Descripcion = tipo.Descripcion,
                TarifaDiaria = tipo.TarifaDiaria,
                Imagen = tipo.Imagen == null ? null : new ImagenDTO
                {
                    IdImagen = tipo.Imagen.IdImagen,
                    Url = tipo.Imagen.Ruta
                }
            });

        }

        // En proceso
        public async Task<object> ActualizarTipoDeHabitacion(TipoDeHabitacionModificarDTO tipoDeHabitacionModificarDTO)
        {
            try
            {
                // Si el atributo Imagen es nulo, no se sube una nueva imagen.
                string? urlImagen = null;

                if (tipoDeHabitacionModificarDTO.Imagen != null)
                {
                    // Se sube la imagen y obtenemos la URL
                    urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(tipoDeHabitacionModificarDTO.Imagen, tipoDeHabitacionModificarDTO.NombreArchivo);
                }

                var tipoDeHabitacion = new TipoDeHabitacion
                {
                    IdTipoDeHabitacion = tipoDeHabitacionModificarDTO.IdTipoDeHabitacion,
                    Nombre = tipoDeHabitacionModificarDTO.Nombre,
                    Descripcion = tipoDeHabitacionModificarDTO.Descripcion,
                    TarifaDiaria = tipoDeHabitacionModificarDTO.TarifaDiaria,
                    Imagen = null
                };

                // Aquí se pasa la lógica para que el repositorio actualice la habitación
                var tipoDeHabitacionActualizada = await this._tarifasRepositorio.ActualizarTipoDeHabitacion(tipoDeHabitacion, urlImagen);





                return tipoDeHabitacionActualizada;  // Retornamos el objeto actualizado (puede ser un DTO o la entidad)
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar tipo de habitación: {ex.Message}");
            }
        }

    }
}
