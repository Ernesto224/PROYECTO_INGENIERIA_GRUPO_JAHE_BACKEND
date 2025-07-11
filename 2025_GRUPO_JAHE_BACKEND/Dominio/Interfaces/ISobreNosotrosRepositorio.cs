﻿using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ISobreNosotrosRepositorio
    {
        public Task<SobreNosotros> VerDatosSobreNosotros();
        public Task<SobreNosotros> CambiarTextoSobreNosotros(SobreNosotros sobreNosotros);
        public Task<SobreNosotros> CambiarImagenGaleriaSobreNosotros(SobreNosotros sobreNosotros, string urlImagen);
    }
}
