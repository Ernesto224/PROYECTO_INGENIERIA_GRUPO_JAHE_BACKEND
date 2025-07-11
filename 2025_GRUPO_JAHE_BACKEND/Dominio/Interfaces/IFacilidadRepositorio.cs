﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IFacilidadRepositorio
    {
        public Task<IEnumerable<Facilidad>> VerInstalacionesYAtractivos();
        public Task<object> ModificarInfromacionDeInstalacionYAtractivo(Facilidad facilidad);
    }
}
