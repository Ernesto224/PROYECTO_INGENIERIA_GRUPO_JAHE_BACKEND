using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IServicioAlmacenamientoImagenes
    {
        public Task<string> SubirImagen(byte[] imagen, string nombreArchivo);
    }
}
