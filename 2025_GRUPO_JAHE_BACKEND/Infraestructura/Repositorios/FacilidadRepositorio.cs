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
    public class FacilidadRepositorio : IFacilidadRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public FacilidadRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<object> ModificarInfromacionDeInstalacionYAtractivo(Facilidad facilidad)
        {
            try
            {
                // Buscar la habitación que se desea actualizar
                var facilidadDb = await this._contexto.Facilidades
                    .Include(facilidad => facilidad.Imagen)  // Incluir la entidad Imagen asociada
                    .FirstOrDefaultAsync(facilidadRecuperada => facilidadRecuperada.IdFacilidad == facilidad.IdFacilidad);

                if (facilidadDb == null)
                    throw new Exception("Facilidad no encontrada.");

                // Actualizar los datos de la habitación
                facilidadDb.Descripcion = facilidad.Descripcion;

                // Si se ha proporcionado una nueva URL de imagen, se actualiza
                if (facilidad.Imagen.Url != null)
                {
                    // Actualizamos la URL de la imagen
                    facilidadDb.Imagen.Url = facilidad.Imagen.Url;
                }

                // Guardamos los cambios en la base de datos
                var resultado = await this._contexto.SaveChangesAsync();

                return resultado > 0 ? new { icon = "success", text = "Datos modificados correctamente." }
                    : new { icon = "error", text = "No se pudieron modificar los datos." };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Facilidad>> VerInstalacionesYAtractivos()
        {
            try
            {
                var facilidades = await this._contexto.Facilidades
                    .Include(facilidad => facilidad.Imagen)
                    .Where(facilidad => facilidad.Imagen!.Eliminado == false)
                    .ToListAsync<Facilidad>();

                if (facilidades == null)
                    throw new Exception("No se encontraron datos.");

                return facilidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
