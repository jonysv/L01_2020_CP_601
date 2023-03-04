using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_CP_601.Models;
using Microsoft.EntityFrameworkCore;
namespace L01_2020_CP_601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogContext _blogContexto;

        public publicacionesController(blogContext blogContexto)
        {
            _blogContexto = blogContexto;

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<publicaciones> listadoPublicaciones = (from e in _blogContexto.publicaciones
                                              select e).ToList();

            if (listadoPublicaciones.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoPublicaciones);
        }

        [HttpGet]
        [Route("FindByUser/{id}")]
        public IActionResult FindByuser(int id)
        {
            publicaciones? publicaciones = (from e in _blogContexto.publicaciones
                                  where e.usuarioId == id
                                  select e).FirstOrDefault();

            if (publicaciones == null)
            {
                return NotFound();
            }

            return Ok(publicaciones);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarPublicacion([FromBody] publicaciones publicaciones)
        {
            try
            {
                _blogContexto.publicaciones.Add(publicaciones);
                _blogContexto.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPublicacion(int id, [FromBody] publicaciones publicacionModificar)
        {
            try
            {
                publicaciones? publicacionActual = (from e in _blogContexto.publicaciones
                                            where e.publicacionId == id
                                            select e).FirstOrDefault();
                if (publicacionActual == null)
                {
                    return NotFound();
                }
                publicacionActual.titulo = publicacionModificar.titulo;
                publicacionActual.descripcion = publicacionModificar.descripcion;
                publicacionActual.usuarioId = publicacionModificar.usuarioId;



                _blogContexto.Entry(publicacionActual).State = EntityState.Modified;
                _blogContexto.SaveChanges();
                return Ok(publicacionActual);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPublicacion(int id)
        {
            try
            {
                publicaciones? publicaciones = (from e in _blogContexto.publicaciones
                                      where e.publicacionId == id
                                      select e).FirstOrDefault();
                if (publicaciones == null)
                {
                    return NotFound();
                }

                _blogContexto.publicaciones.Attach(publicaciones);
                _blogContexto.publicaciones.Remove(publicaciones);
                _blogContexto.SaveChanges();
                return Ok(publicaciones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
