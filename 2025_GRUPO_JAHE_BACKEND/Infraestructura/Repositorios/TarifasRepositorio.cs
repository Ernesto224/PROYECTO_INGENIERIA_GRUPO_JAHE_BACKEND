using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class TarifasRepositorio : ITarifasRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public TarifasRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

      


        public async Task<IEnumerable<TipoDeHabitacion>> verTarifas()
        {
            try
            {
                var TipoDeHabitaciones = await this._contexto.tipoDeHabitacion
                    .Include(th => th.Imagen) // Incluye los datos de la imagen
                    .ToListAsync();

                if (TipoDeHabitaciones == null || !TipoDeHabitaciones.Any())
                    throw new Exception("No se encontraron datos.");


                return TipoDeHabitaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
