using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface ISobreNosotrosServicio
    {
        public Task<SobreNosotrosDTO> VerDatosSobreNosotros();
        public Task<SobreNosotrosDTO> CambiarTextoSobreNosotros(SobreNosotrosDTO sobreNosotrosDTO);
        public Task<SobreNosotrosDTO> CambiarImagenGaleriaSobreNosotros(SobreNosotrosModificarDTO galeriaModificarDTO);
    }
}
