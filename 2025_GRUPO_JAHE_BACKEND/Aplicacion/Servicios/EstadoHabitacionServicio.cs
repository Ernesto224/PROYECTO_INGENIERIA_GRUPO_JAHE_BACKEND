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
    public class EstadoHabitacionServicio : IEstadoHabitacionServicio
    {


        private readonly IEstadoHabitacionRepositorio _estadoHabitacionRepositorio;

        public EstadoHabitacionServicio(IEstadoHabitacionRepositorio estadoHabitacionRepositorio)
        {
            this._estadoHabitacionRepositorio = estadoHabitacionRepositorio;
        }

       

        public async Task<object> ActualizarEstadoDeHabitacion(ActualizarEstadoHabitacionDTO actualizarEstadoHabitacionDTO)
        {
            try
            {
                var resultado = await this._estadoHabitacionRepositorio
                    .ActualizarEstadoDeHabitacion(actualizarEstadoHabitacionDTO.IdHabitacion, actualizarEstadoHabitacionDTO.NuevoEstado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar tipo de habitación: {ex.Message}");
            }
        }





        public async Task<IEnumerable<EstadoHabitacionDTO>> verHabitaciones()
        {


            try
            {
                var habitaciones = await this._estadoHabitacionRepositorio.verHabitaciones();

                return habitaciones.Select(h => new EstadoHabitacionDTO
                {
                    IdHabitacion = h.IdHabitacion,
                    Numero = h.Numero,
                    Estado = h.Estado,
                    TipoDeHabitacion = new TipoDeHabitacionDTO
                    {
                        IdTipoDeHabitacion = h.TipoDeHabitacion.IdTipoDeHabitacion,
                        Nombre = h.TipoDeHabitacion.Nombre,
                        Descripcion = h.TipoDeHabitacion.Descripcion,
                        TarifaDiaria = h.TipoDeHabitacion.TarifaDiaria,
                        Imagen = h.TipoDeHabitacion.Imagen == null ? null : new ImagenDTO
                        {
                            IdImagen = h.TipoDeHabitacion.Imagen.IdImagen,
                            Url = h.TipoDeHabitacion.Imagen.Ruta
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las habitaciones: {ex.Message}");
            }


        }



    }
}
