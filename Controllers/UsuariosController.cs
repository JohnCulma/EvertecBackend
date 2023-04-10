using AutoMapper;
using Data.Context;
using Data.Models.Usuarios;
using EvertecApi.Dto;
using EvertecApi.Helpers.Respuestas;
using EvertecApi.Log4net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvertecApi.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly ILoggerManager log;

        public UsuariosController(AppDbContext context, IMapper mapper, ILoggerManager log)
        {
            this.context = context;
            this.mapper = mapper;
            this.log = log;
        }

        [HttpGet("ConsultarUsuarios", Name = "consultarUsuarios")]
        public async Task<ActionResult<List<ConsultarUsuariosDto>>> Get()
        {
            try
            {
                var usuario = await context.usuarios.ToListAsync();

                if (usuario == null)
                {
                    return BadRequest(new Generales()
                    {
                        title = "Consultar usuarios",
                        status = 400,
                        message = "Usuario no encontrado."
                    });
                }

                var dtos = mapper.Map<List<ConsultarUsuariosDto>>(usuario);

                return dtos;
            }
            catch (Exception ex)
            {
                return BadRequest(new Generales()
                {
                    title = "Consultar usuarios",
                    status = 400,
                    message = ex.Message.ToString()
                });
            }

        }

        [HttpPost("AgregarUsuarios")]
        public async Task<ActionResult> Post([FromBody] AgregarUsuarioDto agregarUsuarioDto)
        {
            try
            {

                var usuario = mapper.Map<Usuarios>(agregarUsuarioDto);

                usuario.Id = Guid.NewGuid().ToString();
                usuario.EmailUsuarioRegistro = "john.culma@outlook.com";
                usuario.FechaRegistro = DateTime.Now;

                usuario.TieneHermanos = agregarUsuarioDto.TieneHermanos;
                    
                context.Add(usuario);
                await context.SaveChangesAsync();

                var usuarioDTO = mapper.Map<AgregarUsuarioDto>(usuario);

                return new CreatedAtRouteResult("consultarUsuarios", new { id = usuarioDTO.Id }, usuarioDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new Generales()
                {
                    title = "Consultar usuarios",
                    status = 400,
                    message = ex.Message.ToString()
                });
            }
        }
       

        [HttpPut("EditarUsuarios")]
        public async Task<ActionResult> Put(AgregarUsuarioDto agregarUsuarioDto)
        {

            try
            {
                if (agregarUsuarioDto.Id != agregarUsuarioDto.Id)
                {
                    return BadRequest("El id no coincide, con id de la url");
                }

                var existe = await context.usuarios.AnyAsync(x => x.Id.Equals(agregarUsuarioDto.Id));

                if (!existe)
                {
                    return NotFound();
                }

                var data = mapper.Map<Usuarios>(agregarUsuarioDto);

                data.Id = agregarUsuarioDto.Id;
                data.EmailUsuarioRegistro = "john.culma@outlook.com";
                data.FechaRegistro = DateTime.Now;
                data.UsuarioModifico = "john.culma@outlook.com";
                data.FechaModifico = DateTime.Now;

                context.Update(data);

                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new Generales()
                {
                    title = "Consultar usuarios",
                    status = 400,
                    message = ex.Message.ToString()
                });
            }
      
        }


        [HttpDelete("EliminarUsuarios/{id:Guid}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {

                var existe = await context.usuarios.AnyAsync(x => x.Id.Equals(id));

                if (!existe)
                {
                    return NotFound();
                }              

                context.Remove(new Usuarios() { Id = id });

                await context.SaveChangesAsync();

                return Ok(new Generales()
                {
                    title = "Eliminar usuarios",
                    status = 200,
                    message = "Usuario eliminado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Generales()
                {
                    title = "Consultar usuarios",
                    status = 400,
                    message = ex.Message.ToString()
                });
            }

        }
    }
}
