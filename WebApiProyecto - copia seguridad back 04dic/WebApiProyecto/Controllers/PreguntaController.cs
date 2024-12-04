using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using WebApiProyecto.Models;

using System.Linq;

using Microsoft.AspNetCore.Cors;


namespace WebApiProyecto.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        public readonly ApitaiContext _dbcontext; // Poder operaciones CRUD en base de datos APITAI

        public PreguntaController(ApitaiContext _context)
        {
            _dbcontext = _context;
        }

        // GET: Obtener listado completo de preguntas
        [HttpGet]
        [Route("ListaPreguntas")]
        public IActionResult Lista()
        {
            try
            {
                // LINQ: Selecciona todas las preguntas y las mapea a DTO
                var lista = _dbcontext.Preguntas
                    .Select(p => new PreguntaDTO
                    {
                        Id = p.Id,
                        Pregunta = p.Pregunta1,
                        Opcion1 = p.Opcion1,
                        Opcion2 = p.Opcion2,
                        Opcion3 = p.Opcion3,
                        Opcion4 = p.Opcion4,
                        Respuesta = p.Respuesta,
                        Bloque = p.Bloque,
                        Tema = p.Tema,
                        Origen = p.Origen
                    })
                    .ToList();

                // Devuelve 200 OK con la lista
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: Obtener pregunta por ID
        [HttpGet]
        [Route("Obtener/{idPregunta:int}")]
        public IActionResult Obtener(int idPregunta)
        {
            try
            {
                var pregunta = _dbcontext.Preguntas
                    .Select(p => new PreguntaDTO
                    {
                        Id = p.Id,
                        Pregunta = p.Pregunta1,
                        Opcion1 = p.Opcion1,
                        Opcion2 = p.Opcion2,
                        Opcion3 = p.Opcion3,
                        Opcion4 = p.Opcion4,
                        Respuesta = p.Respuesta,
                        Bloque = p.Bloque,
                        Tema = p.Tema,
                        Origen = p.Origen
                    })
                    .FirstOrDefault(p => p.Id == idPregunta);

                if (pregunta == null)
                {
                    return NotFound(new { mensaje = "Pregunta no encontrada" });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = pregunta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Obtener preguntas por bloque con estadísticas
        // GET: api/Pregunta/ObtenerPorBloque/B1
        [HttpGet]
        [Route("ObtenerPorBloque/{bloque}")]
        public IActionResult ObtenerPorBloque(string bloque)
        {
            try
            {
                // LINQ: Filtra preguntas por bloque
                var preguntas = _dbcontext.Preguntas
                    .Where(p => p.Bloque == bloque)
                    .Select(p => new PreguntaDTO // Usamos el DTO para evitar data innecesaria
                    {
                        Id = p.Id,
                        Pregunta = p.Pregunta1,
                        Opcion1 = p.Opcion1,
                        Opcion2 = p.Opcion2,
                        Opcion3 = p.Opcion3,
                        Opcion4 = p.Opcion4,
                        Respuesta = p.Respuesta,
                        Bloque = p.Bloque,
                        Tema = p.Tema,
                        Origen = p.Origen
                    })
                    .OrderBy(p => p.Id)
                    .ToList();

                if (!preguntas.Any())
                {
                    return NotFound(new { mensaje = $"No se encontraron preguntas para el bloque {bloque}" });
                }

                // Calcula estadísticas del bloque
                var estadisticas = new
                {
                    TotalPreguntas = preguntas.Count,
                    Temas = preguntas.Select(p => p.Tema).Distinct().Count(), // Cuenta temas unicos en el bloque
                    PreguntasPorTema = preguntas  // Agrupa preguntas por tema y cuenta
                        .GroupBy(p => p.Tema)
                        .Select(g => new
                        {
                            Tema = g.Key,
                            CantidadPreguntas = g.Count()
                        })
                        .OrderBy(x => x.Tema)
                        .ToList()
                };

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "ok",
                    bloque = bloque,
                    estadisticas = estadisticas,
                    preguntas = preguntas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: api/Pregunta/Bloques
        [HttpGet]
        [Route("ListaBloques")]
        public IActionResult ObtenerBloques()
        {
            try
            {
                // LINQ: Agrupa preguntas por bloque y calcula estadística
                var bloques = _dbcontext.Preguntas
                    .GroupBy(p => p.Bloque)
                    .Select(g => new
                    {
                        Bloque = g.Key,
                        TotalPreguntas = g.Count(),
                        Temas = g.Select(p => p.Tema).Distinct().Count()
                    })
                    .OrderBy(b => b.Bloque)
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", bloques = bloques });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        
        // OBTENER PREGUNTAs por TEMA
        // GET: api/Pregunta/ObtenerPorTema/{tema}
        [HttpGet]
        [Route("ObtenerPorTema/{tema}")]
        public IActionResult ObtenerPorTema(string tema)
        {
            try
            {
                tema = Uri.UnescapeDataString(tema); // Soluciona el error al cargar tema por caracteres especiales

                var preguntas = _dbcontext.Preguntas
                    .Where(p => p.Tema == tema)
                    .Select(p => new PreguntaDTO
                    {
                        Id = p.Id,
                        Pregunta = p.Pregunta1,
                        Opcion1 = p.Opcion1,
                        Opcion2 = p.Opcion2,
                        Opcion3 = p.Opcion3,
                        Opcion4 = p.Opcion4,
                        Respuesta = p.Respuesta,
                        Bloque = p.Bloque,
                        Tema = p.Tema,
                        Origen = p.Origen
                    })
                    .OrderBy(p => p.Id)
                    .ToList();

                if (!preguntas.Any())
                {
                    return NotFound(new { mensaje = $"No se encontraron preguntas para el tema {tema}" });
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "ok",
                    tema = tema,
                    preguntas = preguntas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: api/Pregunta/Temas
        [HttpGet]
        [Route("ListaTemas")]
        public IActionResult ObtenerTemas()
        {
            try
            {
                var temas = _dbcontext.Preguntas
                    .GroupBy(p => p.Tema)
                    .Select(g => new
                    {
                        Tema = g.Key,
                        TotalPreguntas = g.Count(),
                        
                    })
                    .OrderBy(t => t.Tema)
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "ok",
                    temas = temas
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] PreguntaGuardarDTO dto)
        {
            try
            {
                var pregunta = new Pregunta  // Crea nueva entidad Pregunta desde DTO

                {
                    Pregunta1 = dto.Pregunta,
                    Opcion1 = dto.Opcion1,
                    Opcion2 = dto.Opcion2,
                    Opcion3 = dto.Opcion3,
                    Opcion4 = dto.Opcion4,
                    Respuesta = dto.Respuesta,
                    Bloque = dto.Bloque,
                    Tema = dto.Tema,
                    Origen = dto.Origen
                };

                _dbcontext.Preguntas.Add(pregunta);
                _dbcontext.SaveChanges();

                // Retorna la pregunta creada como DTO
                return StatusCode(StatusCodes.Status201Created, new
                {
                    mensaje = "Pregunta guardada correctamente",
                    response = new PreguntaDTO // Usamos el DTO de respuesta
                    {
                        Id = pregunta.Id,
                        Pregunta = pregunta.Pregunta1,
                        Opcion1 = pregunta.Opcion1,
                        Opcion2 = pregunta.Opcion2,
                        Opcion3 = pregunta.Opcion3,
                        Opcion4 = pregunta.Opcion4,
                        Respuesta = pregunta.Respuesta,
                        Bloque = pregunta.Bloque,
                        Tema = pregunta.Tema,
                        Origen = pregunta.Origen
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // MODIFICAR UNA PREGUNTA
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] PreguntaEditarDTO dto)
        {
            try
            {
                var pregunta = _dbcontext.Preguntas.Find(dto.Id);

                if (pregunta == null)
                {
                    return NotFound(new { mensaje = "Pregunta no encontrada" });
                }

                // Actualizar los campos
                pregunta.Pregunta1 = dto.Pregunta;
                pregunta.Opcion1 = dto.Opcion1;
                pregunta.Opcion2 = dto.Opcion2;
                pregunta.Opcion3 = dto.Opcion3;
                pregunta.Opcion4 = dto.Opcion4;
                pregunta.Respuesta = dto.Respuesta;
                pregunta.Bloque = dto.Bloque;
                pregunta.Tema = dto.Tema;
                pregunta.Origen = dto.Origen;

                _dbcontext.Preguntas.Update(pregunta);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "Pregunta actualizada correctamente",
                    response = new PreguntaDTO
                    {
                        Id = pregunta.Id,
                        Pregunta = pregunta.Pregunta1,
                        Opcion1 = pregunta.Opcion1,
                        Opcion2 = pregunta.Opcion2,
                        Opcion3 = pregunta.Opcion3,
                        Opcion4 = pregunta.Opcion4,
                        Respuesta = pregunta.Respuesta,
                        Bloque = pregunta.Bloque,
                        Tema = pregunta.Tema,
                        Origen = pregunta.Origen
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // BORRAR PREGUNTAS
        [HttpDelete]
        [Route("Eliminar/{idPregunta:int}")]
        public IActionResult Eliminar(int idPregunta)
        {
            try
            {
                // Obtener pregunta incluyendo sus resultados asociados!!! (tabla resultados)
                var oPregunta = _dbcontext.Preguntas
                    .Include(p => p.Resultados)
                    .FirstOrDefault(p => p.Id == idPregunta);

                if (oPregunta == null)
                {
                    return BadRequest("Pregunta no encontrada");
                }

                // Eliminar resultados asociados de mi tabla RESULTADOS
                _dbcontext.Resultados.RemoveRange(oPregunta.Resultados);

                _dbcontext.Preguntas.Remove(oPregunta);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Pregunta y resultados asociados eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al eliminar la pregunta", error = ex.Message });
            }
        }
    }
}
