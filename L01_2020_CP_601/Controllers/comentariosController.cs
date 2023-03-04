using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_CP_601.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_CP_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public comentariosController(blogContext blogContexto)
        {
            _blogContexto = blogContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadoComentario = (from e in _blogContexto.comentarios
                                                        select e).ToList();

            if (listadoComentario.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoComentario);
        }

        [HttpGet]
        [Route("FindByPublicacion/{id}")]
        public IActionResult FindByPublicacion(int id)
        {
            comentarios? comentarios = (from e in _blogContexto.comentarios
                                            where e.publicacionId == id
                                            select e).FirstOrDefault();

            if (comentarios == null)
            {
                return NotFound();
            }

            return Ok(comentarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarComentario([FromBody] comentarios comentarios)
        {
            try
            {
                _blogContexto.comentarios.Add(comentarios);
                _blogContexto.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarComentarios(int id, [FromBody] comentarios comentariosActualizar)
        {
            try
            {
                comentarios? comentariosActual = (from e in _blogContexto.comentarios
                                                  where e.cometarioId == id
                                                  select e).FirstOrDefault();
                if (comentariosActual == null)
                {
                    return NotFound();
                }
                comentariosActual.publicacionId = comentariosActualizar.publicacionId;
                comentariosActual.comentario = comentariosActualizar.comentario;
                comentariosActual.usuarioId = comentariosActualizar.usuarioId;



                _blogContexto.Entry(comentariosActual).State = EntityState.Modified;
                _blogContexto.SaveChanges();
                return Ok(comentariosActual);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarComentario(int id)
        {
            try
            {
                comentarios? comentarios = (from e in _blogContexto.comentarios
                                            where e.cometarioId == id
                                            select e).FirstOrDefault();
                if (comentarios == null)
                {
                    return NotFound();
                }

                _blogContexto.comentarios.Attach(comentarios);
                _blogContexto.comentarios.Remove(comentarios);
                _blogContexto.SaveChanges();
                return Ok(comentarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
