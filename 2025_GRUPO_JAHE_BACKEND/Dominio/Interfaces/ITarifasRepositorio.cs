﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ITarifasRepositorio
    {
        public Task<IEnumerable<TipoDeHabitacion>> verTarifas();
        public Task<object> ActualizarTipoDeHabitacion(TipoDeHabitacion tipoDeHabitacion, string? urlImagen);
    }
}
