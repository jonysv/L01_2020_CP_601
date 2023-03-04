using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_CP_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_CP_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public usuariosController(blogContext blogContexto)
        {
            _blogContexto = blogContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuarios = (from e in _blogContexto.usuarios
                                           select e).ToList();

            if (listadoUsuarios.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpGet]
        [Route("FindByNombre/{filtro}")]
        public IActionResult FindByNombre(string filtro)
        {
            usuarios? usuarios = (from e in _blogContexto.usuarios
                               where e.nombre.Contains(filtro)
                               select e).FirstOrDefault();

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }
        
        [HttpGet]
        [Route("FindByApellido/{filtro}")]
        public IActionResult FindByApellido(string filtro)
        {
            usuarios? usuarios = (from e in _blogContexto.usuarios
                                  where e.apellido.Contains(filtro)
                                  select e).FirstOrDefault();

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        [HttpGet]
        [Route("FindByRol/{idRol}")]
        public IActionResult FindByRol(int idRol)
        {
            usuarios? usuarios = (from e in _blogContexto.usuarios
                                  where e.rolId == idRol
                                  select e).FirstOrDefault();

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsuario([FromBody] usuarios usuarios)
        {
            try
            {
                _blogContexto.usuarios.Add(usuarios);
                _blogContexto.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] usuarios usuariosModificar)
        {
            try
            {
                usuarios? usuariosActual = (from e in _blogContexto.usuarios
                                         where e.usuarioId == id
                                         select e).FirstOrDefault();
                if (usuariosActual == null)
                {
                    return NotFound();
                }
                usuariosActual.rolId = usuariosModificar.rolId;
                usuariosActual.nombreUsuario = usuariosModificar.nombreUsuario;
                usuariosActual.clave = usuariosModificar.clave;
                usuariosActual.nombre = usuariosModificar.nombre;
                usuariosActual.apellido = usuariosModificar.apellido;


                _blogContexto.Entry(usuariosActual).State = EntityState.Modified;
                _blogContexto.SaveChanges();
                return Ok(usuariosActual);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            try
            {
                usuarios? usuarios = (from e in _blogContexto.usuarios
                                   where e.usuarioId == id
                                   select e).FirstOrDefault();
                if (usuarios == null)
                {
                    return NotFound();
                }

                _blogContexto.usuarios.Attach(usuarios);
                _blogContexto.usuarios.Remove(usuarios);
                _blogContexto.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
