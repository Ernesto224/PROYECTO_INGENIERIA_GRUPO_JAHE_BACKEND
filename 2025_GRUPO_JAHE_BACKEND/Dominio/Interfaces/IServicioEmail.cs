﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IServicioEmail
    {
        Task<bool> enviarEmail(string email, string asunto, string mensaje);
    }
}
