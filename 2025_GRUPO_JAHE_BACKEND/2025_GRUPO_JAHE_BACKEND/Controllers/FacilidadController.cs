﻿using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilidadController : ControllerBase
    {
        private readonly IFacilidadServicio _facilidadServicio;

        public FacilidadController(IFacilidadServicio facilidadServicio)
        {
            this._facilidadServicio = facilidadServicio;
        }

        // GET: api/<FacilidadController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilidadDTO>>> Get()
        {
            try
            {
                var facilidadesDTO = await this._facilidadServicio.VerInstalacionesYAtractivos();

                if (facilidadesDTO == null)
                    return NotFound("No se encontraron datos.");

                return Ok(facilidadesDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<FacilidadController>/5
        [HttpPut]
        public async Task<ActionResult<Object>> Put(FacilidadModificarDTO facilidadModificarDTO)
        {
            try 
            { 
                var resultado = await this._facilidadServicio.ModificarInfromacionDeInstalacionYAtractivo(facilidadModificarDTO);
                return Ok(resultado);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
