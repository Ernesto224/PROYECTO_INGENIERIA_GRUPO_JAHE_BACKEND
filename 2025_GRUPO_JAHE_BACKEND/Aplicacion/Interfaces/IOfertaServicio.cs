﻿using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IOfertaServicio
    {
        public Task<List<OfertaDTO>> VerOfertasActivas();
    }
}
