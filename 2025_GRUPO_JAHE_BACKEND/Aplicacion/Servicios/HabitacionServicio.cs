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
    public class HabitacionServicio : IHabitacionServicio
    {
        private readonly IHabitacionRepositorio _habitacionRepositorio;

        public HabitacionServicio ( IHabitacionRepositorio habitacionRepositorio)
        {
            this._habitacionRepositorio = habitacionRepositorio;
        }

        public async Task<ResultadoConsultaHabitacionDTO> ConsultarDisponibilidadDeHabitaciones(int[] idTiposHabitacion, 
            DateTime fechaLlegada, DateTime fechaSalida, int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            var resultado = await this._habitacionRepositorio.ConsultarDisponibilidadDeHabitaciones(idTiposHabitacion,
                fechaLlegada, fechaSalida, numeroDePagina, maximoDeDatos, irALaUltimaPagina);

            if (resultado.habitaciones == null)
                throw new Exception("No se encontraron datos.");

            return new ResultadoConsultaHabitacionDTO
            {
                Habitaciones = resultado.habitaciones.Select(h => new HabitacionConsultaDTO 
                    {
                        IdHabitacion = h.IdHabitacion,
                        Numero = h.Numero,
                        TipoDeHabitacion = new TipoDeHabitacionConsultaDTO 
                        { 
                            IdTipoDeHabitacion = h.TipoDeHabitacion.IdTipoDeHabitacion,
                            Nombre = h.TipoDeHabitacion.Nombre,
                            TarifaDiaria = h.TipoDeHabitacion.TarifaDiaria,
                        }
                    }
                ),
                TotalRegistros = resultado.datosTotales,
                PaginaActual = resultado.paginaActual,
                MaximoPorPagina = maximoDeDatos
            };

        }
    }
}
