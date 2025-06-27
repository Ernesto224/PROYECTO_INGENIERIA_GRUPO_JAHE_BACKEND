using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class AdministradorLoginDTO
    {
        public string NombreDeUsuario { get; set; }
        public string Contrasennia { get; set; }
    }
}
