﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class TipoDeHabitacionConsultaDTO
    {
        public int IdTipoDeHabitacion { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal TarifaDiaria { get; set; }
    }
}
