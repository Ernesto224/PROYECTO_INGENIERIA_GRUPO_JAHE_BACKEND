using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class SobreNosotrosRepositorio : ISobreNosotrosRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public SobreNosotrosRepositorio(ContextoDbSQLServer contexto)
        {
            _contexto = contexto;
        }

        public async Task<SobreNosotros> CambiarTextoSobreNosotros(SobreNosotros sobreNosotros)
        {
            try
            {
                var resultado = await this._contexto.SobreNosotros.Include(img=> img.ImagenesSobreNosotros).ThenInclude(imagen=> imagen.Imagen).FirstOrDefaultAsync();
                if (resultado != null)
                {
                    resultado.Descripcion = sobreNosotros.Descripcion;
                    await this._contexto.SaveChangesAsync();
                }
                return resultado;

            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Repository - Error de operación inválida: {ex.Message}");
                throw new Exception("Error en la consulta a la base de datos", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository - Error inesperado: {ex.Message}");
                throw new Exception("Error al actualizar los datos de SobreNosotros", ex);
            }
        }

        public async Task<SobreNosotros> VerDatosSobreNosotros()
        {
            try
            {
                Console.WriteLine("Repository - Obteniendo datos de SobreNosotros");

                var resultado = await _contexto.SobreNosotros
                    .Include(sn => sn.ImagenesSobreNosotros)
                    .ThenInclude(isn => isn.Imagen)
                    .FirstOrDefaultAsync();

                if (resultado == null)
                {
                    Console.WriteLine("Repository - No se encontraron registros de SobreNosotros");
                }

                return resultado;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Repository - Error de operación inválida: {ex.Message}");
                throw new Exception("Error en la consulta a la base de datos", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Repository - Error inesperado: {ex.Message}");
                throw new Exception("Error al obtener los datos de SobreNosotros", ex);
            }
        }
    }
}
