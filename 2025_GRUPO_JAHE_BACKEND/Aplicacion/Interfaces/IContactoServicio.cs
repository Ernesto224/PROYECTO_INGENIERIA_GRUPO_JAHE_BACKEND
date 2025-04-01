﻿using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IContactoServicio
    {
        public Task<ContactoDTO> VerDatosContacto();
    }
}
