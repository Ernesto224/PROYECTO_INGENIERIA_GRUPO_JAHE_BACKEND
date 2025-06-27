using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ReservaCompletaDTO
    {
        public List<ReservaDTO> ReservasDTO { get; set; }

        public ClienteDTO ClienteDTO { get; set; }

    }
}
