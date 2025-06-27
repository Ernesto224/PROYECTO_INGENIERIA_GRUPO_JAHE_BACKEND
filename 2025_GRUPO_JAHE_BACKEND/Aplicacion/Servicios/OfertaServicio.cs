using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class OfertaServicio : IOfertaServicio
    {
        private readonly IOfertaRepositorio _ofertaRepositorio;

        private readonly ITransactionMethods _unitOfWork;

        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public OfertaServicio(IOfertaRepositorio ofertaRepositorio, ITransactionMethods unitOfWork, IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            _ofertaRepositorio = ofertaRepositorio;
            _unitOfWork = unitOfWork;
            _servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<RespuestaDTO<OfertaDTO>> CrearOferta(OfertaCreacionDTO ofertaCreacionDTO)
        {
            try
            {
                var ofertaDTO = ofertaCreacionDTO.ofertaDTO;

                var oferta = new Oferta()
                {
                    Nombre = ofertaDTO.Nombre,
                    FechaInicio = ofertaDTO.FechaInicio,
                    FechaFinal = ofertaDTO.FechaFinal,
                    Porcentaje = ofertaDTO.Porcentaje,
                    Activa = true,
                    IdTipoDeHabitacion = ofertaDTO.TipoDeHabitacion.IdTipoDeHabitacion
                };

                if (ofertaCreacionDTO.Imagen != null)
                {
                    string urlImagen = await _servicioAlmacenamientoImagenes.SubirImagen(
                        ofertaCreacionDTO.Imagen,
                        ofertaCreacionDTO.NombreArchivo);

                    oferta.Imagen = new Imagen
                    {
                        Ruta = urlImagen
                    };
                }
                else if (ofertaDTO.Imagen != null)
                {
                    oferta.IdImagen = ofertaDTO.Imagen.IdImagen;
                }

                await _ofertaRepositorio.CrearAsync(oferta);
                await _unitOfWork.SaveChangesAsync();

                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = "Oferta creada correctamente",
                    EsCorrecto = true,
                    Objeto = ofertaDTO
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = $"Error creando la oferta: {ex.Message}, {ex.InnerException}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
            
        }

        public async Task<RespuestaDTO<OfertaDTO>> EliminarOferta(int idOferta)
        {
            try
            {
                var oferta = await this._ofertaRepositorio.VerOfertaPorId(idOferta);

                await this._ofertaRepositorio.DeleteAsync(oferta);

                var resultado = await this._unitOfWork.SaveChangesAsync();


                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = "Estado actualizado correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };

            } 
            catch (Exception ex)
            {
                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = $"Error eliminando oferta: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
            
        }

        public async Task<RespuestaDTO<OfertaDTO>> ModificarOferta(OfertaModificarDTO ofertaModificarDTO)
        {
            try
            {

                var ofertaDTO = ofertaModificarDTO.ofertaDTO;
                var oferta = await this._ofertaRepositorio.VerOfertaPorId(ofertaDTO.IdOferta);

                oferta.Nombre = ofertaDTO.Nombre;
                oferta.FechaInicio = ofertaDTO.FechaInicio;
                oferta.FechaFinal = ofertaDTO.FechaFinal;
                oferta.Porcentaje = ofertaDTO.Porcentaje;
                oferta.Activa = ofertaDTO.Activo;
                oferta.IdTipoDeHabitacion = ofertaDTO.TipoDeHabitacion.IdTipoDeHabitacion;

                string? urlImagen = null;

                if (ofertaModificarDTO.Imagen != null)
                {
                   
                    urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(ofertaModificarDTO.Imagen, ofertaModificarDTO.NombreArchivo);
                    
                    oferta.Imagen = new Imagen
                    {
                        Ruta = urlImagen
                    };                
                }
                else
                {
                    //oferta.IdImagen = ofertaDTO.Imagen.IdImagen;
                }

                await this._ofertaRepositorio.UpdateAsync(oferta);

                var resultado = await this._unitOfWork.SaveChangesAsync();


                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = "Oferta modificada correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };

            }
            catch (Exception ex)
            {
                return new RespuestaDTO<OfertaDTO>
                {
                    Texto = $"Error modificando oferta: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
        }

        public async Task<RespuestaConsultaDTO<OfertaDTO>> VerOfertas(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            var resultado = await _ofertaRepositorio.VerOfertas(numeroDePagina, maximoDeDatos, irALaUltimaPagina);

            return new RespuestaConsultaDTO<OfertaDTO>
            {
                Lista = resultado.ofertas.Select(h => new OfertaDTO
                {
                    IdOferta = h.IdOferta,
                    Nombre = h.Nombre,
                    FechaInicio = h.FechaInicio,
                    FechaFinal = h.FechaFinal,
                    Activo = h.Activa,
                    Porcentaje = h.Porcentaje,
                    TipoDeHabitacion = new TipoDeHabitacionDTO { IdTipoDeHabitacion = h.TipoDeHabitacion.IdTipoDeHabitacion, Nombre = h.TipoDeHabitacion.Nombre },
                    Imagen = new ImagenDTO { IdImagen = h.Imagen.IdImagen, Url = h.Imagen.Ruta }

                }
                ),
                TotalRegistros = resultado.datosTotales,
                PaginaActual = resultado.paginaActual,
                MaximoPorPagina = maximoDeDatos
            };
        }

        public async Task<List<OfertaDTO>> VerOfertasActivas()
        {
            var ofertas = await this._ofertaRepositorio.VerOfertasActivas();

            if (ofertas == null || !ofertas.Any())
            {
                return null;
            }
            else
            {
                List<OfertaDTO> ofertasActivas = new List<OfertaDTO>();
                foreach (var oferta in ofertas)
                {
                    ofertasActivas.Add(new OfertaDTO
                    {
                        IdOferta = oferta.IdOferta,
                        Nombre = oferta.Nombre,
                        FechaInicio = oferta.FechaInicio,
                        FechaFinal = oferta.FechaFinal,
                        Activo = oferta.Activa,
                        Porcentaje = oferta.Porcentaje,
                        TipoDeHabitacion = new TipoDeHabitacionDTO { IdTipoDeHabitacion = oferta.TipoDeHabitacion.IdTipoDeHabitacion },
                        Imagen = new ImagenDTO { Url = oferta.Imagen.Ruta }
                    });
                }
                return ofertasActivas;
            }
        }
    }
}
