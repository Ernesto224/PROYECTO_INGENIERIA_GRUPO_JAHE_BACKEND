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
    public class HomeRepositorio : IHomeRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public HomeRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<object> ModificarDatosDeHome(Home home)
        {
            try
            {
                // Buscar datos
                var homeDb = await this._contexto.Homes
                    .Include(home => home.Imagen)
                    .FirstOrDefaultAsync<Home>(homeDb => homeDb.IdHome == home.IdHome);

                // Verificar si existen datos
                if (homeDb == null) throw new Exception("No se encontraron datos.");

                // Actualizar datos
                homeDb.Descripcion = home.Descripcion;
                homeDb.Imagen!.Url = home.Imagen!.Url;

                // Guardar cambios
                var resultado = await _contexto.SaveChangesAsync();

                // Retornar mensaje
                return resultado > 0 ? new { tipo = "Ok", mensaje = "Datos modificados correctamente." } 
                    : new { tipo = "Error", mensaje = "No se pudieron modificar los datos." };
            }
            catch (Exception ex)
            {
                return ("Error", ex.Message);
            }
        }

        public async Task<Home> VerDatosDeHome()
        {
            try 
            { 
                var home = await this._contexto.Homes
                    .Include(home => home.Imagen)
                    .Where(home => home.Imagen!.Eliminado == false)
                    .FirstOrDefaultAsync<Home>();

                if (home == null)
                    throw new Exception("No se encontraron datos.");

                return home;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
