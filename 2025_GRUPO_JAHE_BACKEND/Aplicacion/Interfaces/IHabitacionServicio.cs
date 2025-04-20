using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IHabitacionServicio
    {
        public Task<ResultadoConsultaHabitacionDTO> ConsultarDisponibilidadDeHabitaciones(int[] idTiposHabitacion,
            DateTime fechaLlegada, DateTime fechaSalida, int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina);
    }
}
