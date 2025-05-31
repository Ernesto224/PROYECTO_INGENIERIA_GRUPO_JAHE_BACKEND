using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Servicios_de_Dominio
{
    public class CalcularPrecioService
    {
        public decimal AplicarTemporada(decimal montoBase, Temporada temporada)
        {
            return montoBase * (1 + (decimal)temporada.Porcentaje / 100);
        }
    }
}
