﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;

namespace Aplicacion.Interfaces
{
    public interface IPublicidadServicio
    {
        public Task<List<PublicidadDTO>> VerPublicidadesActivas();
    }
}
