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

        public async Task<SobreNosotros> CambiarImagenGaleriaSobreNosotros(SobreNosotros sobreNosotros, string? imagenURL)
        {
            try
            {
                var resultado = await _contexto.SobreNosotros
                    .Include(img => img.ImagenesSobreNosotros)
                    .ThenInclude(imagen => imagen.Imagen)
                    .FirstOrDefaultAsync();

                if (resultado != null && imagenURL != null)
                {
                    foreach (var imagen in resultado.ImagenesSobreNosotros)
                    {
                        if (imagen.Imagen != null) 
                        {
                            var imagenExistente = sobreNosotros.ImagenesSobreNosotros
                                .FirstOrDefault(img => img.IdImagen == imagen.IdImagen);

                            if (imagenExistente != null)
                            {
                                imagen.Imagen.Ruta = imagenURL;
                            }
                        }
                    }
                    await _contexto.SaveChangesAsync();
                }
                return resultado;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error en la consulta a la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar los datos de SobreNosotros", ex);
            }
        }

        public async Task<SobreNosotros> CambiarTextoSobreNosotros(SobreNosotros sobreNosotros)
        {
            try
            {
                var resultado = await this._contexto.SobreNosotros
                    .Include(img => img.ImagenesSobreNosotros)
                    .ThenInclude(imagen => imagen.Imagen)
                    .FirstOrDefaultAsync();

                if (resultado != null)
                {
                    resultado.Descripcion = sobreNosotros.Descripcion;
                    await this._contexto.SaveChangesAsync();
                }

                return resultado;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error en la consulta a la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar los datos de SobreNosotros", ex);
            }
        }

        public async Task<SobreNosotros> VerDatosSobreNosotros()
        {
            try
            {
                var resultado = await _contexto.SobreNosotros
                    .Include(sn => sn.ImagenesSobreNosotros)
                    .ThenInclude(isn => isn.Imagen)
                    .FirstOrDefaultAsync();

                if (resultado == null)
                {
                    throw new Exception("No se encontraron registros de SobreNosotros");
                }
                return resultado;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error en la consulta a la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de SobreNosotros", ex);
            }
        }
    }
}
