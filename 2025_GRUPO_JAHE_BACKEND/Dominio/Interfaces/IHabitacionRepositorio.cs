﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IHabitacionRepositorio
    {
        public void ActualizarHabitacionesOcupadasConTimeout();
        public Task<(IEnumerable<Habitacion> habitaciones, int datosTotales, int paginaActual)> ConsultarDisponibilidadDeHabitaciones(int[] idTiposHabitacion,
            DateTime fechaLlegada, DateTime fechaSalida, int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina);

        public Task<(IEnumerable<Habitacion> habitaciones, int datosTotales, int paginaActual)> ConsultarDisponibilidadDeHabitacionesHoy(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina);
    }
}
