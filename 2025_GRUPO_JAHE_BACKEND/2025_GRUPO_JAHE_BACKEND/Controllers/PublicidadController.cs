﻿using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicidadController : Controller
    {
        private readonly IPublicidadServicio _publicidadServicio;

        public PublicidadController(IPublicidadServicio publicidadServicio)
        {
            this._publicidadServicio = publicidadServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublicidadDTO>>> Get()
        {
            try
            {
                var publicidadDTO = await this._publicidadServicio.VerPublicidadesActivas();

                if (publicidadDTO == null)
                {
                    return NotFound("No se encontraron publicidades.");
                }

                return Ok(publicidadDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
