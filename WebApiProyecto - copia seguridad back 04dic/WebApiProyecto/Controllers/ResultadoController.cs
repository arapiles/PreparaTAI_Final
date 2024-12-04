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
    public class ResultadoController : ControllerBase
    {
        private readonly ApitaiContext _dbcontext;

        public ResultadoController(ApitaiContext context)
        {
            _dbcontext = context;
        }

        // GET: api/Resultado/Lista
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            try
            {
                // LINQ: Obtiene resultados y mapea a DTO incluyendo datos de Pregunta y Usuario
                var lista = _dbcontext.Resultados
                    .Select(r => new ResultadoDTO
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        PreguntaId = r.PreguntaId,
                        Acierto = r.Acierto,
                        Fecha = r.Fecha,
                        // Incluimos solo la información necesaria de la pregunta
                        TextoPregunta = r.Pregunta.Pregunta1,
                        Bloque = r.Pregunta.Bloque,
                        Tema = r.Pregunta.Tema,
                        // Incluimos solo el nombre del usuario
                        NombreUsuario = r.Usuario.Nombre
                    })
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: api/Resultado/Obtener/5
        [HttpGet]
        [Route("Obtener/{idResultado:int}")]
        public IActionResult Obtener(int idResultado)
        {
            try
            {
                var resultado = _dbcontext.Resultados
                    .Where(r => r.Id == idResultado)
                    .Select(r => new ResultadoDTO
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        PreguntaId = r.PreguntaId,
                        Acierto = r.Acierto,
                        Fecha = r.Fecha,
                        // Información de la pregunta
                        TextoPregunta = r.Pregunta.Pregunta1,
                        Bloque = r.Pregunta.Bloque,
                        Tema = r.Pregunta.Tema,
                        // Información del usuario
                        NombreUsuario = r.Usuario.Nombre
                    })
                    .FirstOrDefault();

                if (resultado == null)
                {
                    return NotFound(new { mensaje = "Resultado no encontrado" });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = resultado });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: api/Resultado/ResultadosUsuario/5
        [HttpGet]
        [Route("ResultadosUsuario/{idUsuario:int}")]
        public IActionResult ResultadosUsuario(int idUsuario)
        {
            try
            {
                var resultados = _dbcontext.Resultados
                    .Where(r => r.UsuarioId == idUsuario)
                    .Select(r => new ResultadoDTO
                    {
                        Id = r.Id,
                        UsuarioId = r.UsuarioId,
                        PreguntaId = r.PreguntaId,
                        Acierto = r.Acierto,
                        Fecha = r.Fecha,
                        TextoPregunta = r.Pregunta.Pregunta1,
                        Bloque = r.Pregunta.Bloque,
                        Tema = r.Pregunta.Tema,
                        NombreUsuario = r.Usuario.Nombre
                    })
                    .OrderByDescending(r => r.Fecha)
                    .ToList();

                if (!resultados.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron resultados para este usuario" });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", resultados = resultados });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // ESTADISTICAS

        // GET: api/Resultado/EstadisticasBloques/5
        [HttpGet]
        [Route("EstadisticasBloques/{idUsuario:int}")]
        public IActionResult EstadisticasBloques(int idUsuario)
        {
            try
            {
                // LINQ: Carga resultados con sus preguntas relacionadas
                var resultados = _dbcontext.Resultados
                    .Include(r => r.Pregunta) // Include  carga la pregunta
                    .Where(r => r.UsuarioId == idUsuario)
                    .ToList();

                if (!resultados.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron resultados para este usuario" });
                }

                // Calcula estadísticas por bloque
                var estadisticas = new
                {
                    // Estadisticas globales
                    TotalPreguntas = resultados.Count, // Total de preguntas respondidas
                    Aciertos = resultados.Count(r => r.Acierto), // Num de respuestas correctas
                    Fallos = resultados.Count(r => !r.Acierto), // Incorrectas
                    PorcentajeAciertos = resultados.Any() // Verifica si hay resultados
                        ? Math.Round( // Si hay resultados, calcula el porcentaje
                            (double)resultados.Count(r => r.Acierto) / resultados.Count * 100, 2) // Num aciertos/totalPreguntas x 100 para porcentaje y redondeado a 2 decimales
                        : 0, // Si no hay resultados devuelve 0
                    ResultadosPorBloque = resultados
                        .GroupBy(r => r.Pregunta.Bloque)
                        .Select(g => new {
                            Bloque = g.Key,
                            Total = g.Count(),
                            Aciertos = g.Count(r => r.Acierto),
                            Fallos = g.Count(r => !r.Acierto),
                            PorcentajeAciertos = Math.Round((double)g.Count(r => r.Acierto) / g.Count() * 100, 2)
                        })
                        //.OrderByDescending(x => x.PorcentajeAciertos) Prefiero ordenarlo en el front
                        .ToList()
                };

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", estadisticas = estadisticas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // GET: api/Resultado/EstadisticasTemas/5
        [HttpGet]
        [Route("EstadisticasTemas/{idUsuario:int}")]
        public IActionResult EstadisticasTemas(int idUsuario)
        {
            try
            {
                var resultados = _dbcontext.Resultados
                    .Include(r => r.Pregunta) 
                    .Where(r => r.UsuarioId == idUsuario)
                    .ToList();

                if (!resultados.Any())
                {
                    return NotFound(new { mensaje = "No se encontraron resultados para este usuario" });
                }

                var estadisticas = new
                {
                    TotalPreguntas = resultados.Count,
                    Aciertos = resultados.Count(r => r.Acierto),
                    Fallos = resultados.Count(r => !r.Acierto),
                    PorcentajeAciertos = resultados.Any()
                        ? Math.Round((double)resultados.Count(r => r.Acierto) / resultados.Count * 100, 2)
                        : 0,
                    ResultadosPorTema = resultados
                        .GroupBy(r => r.Pregunta.Tema)
                        .Select(g => new {
                            Tema = g.Key,
                            Total = g.Count(),
                            Aciertos = g.Count(r => r.Acierto),
                            Fallos = g.Count(r => !r.Acierto),
                            PorcentajeAciertos = Math.Round((double)g.Count(r => r.Acierto) / g.Count() * 100, 2)
                        })
                        .OrderByDescending(x => x.PorcentajeAciertos)
                        .ToList()
                };

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", estadisticas = estadisticas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // POST: api/Resultado/Guardar
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] ResultadoGuardarDTO modelo)
        {
            try
            {
                // Verificar que exista el usuario
                var usuario = _dbcontext.Usuarios.Find(modelo.UsuarioId);
                if (usuario == null)
                {
                    return BadRequest(new { mensaje = "Usuario no encontrado" });
                }

                // Verificar que exista la pregunta
                var pregunta = _dbcontext.Preguntas.Find(modelo.PreguntaId);
                if (pregunta == null)
                {
                    return BadRequest(new { mensaje = "Pregunta no encontrada" });
                }

                // Crear el nuevo resultado
                var resultado = new Resultado
                {
                    UsuarioId = modelo.UsuarioId,
                    PreguntaId = modelo.PreguntaId,
                    Acierto = modelo.Acierto,
                    Fecha = DateTime.Now
                };

                _dbcontext.Resultados.Add(resultado);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new
                {
                    mensaje = "Resultado guardado correctamente",
                    response = new
                    {
                        resultado.Id,
                        resultado.UsuarioId,
                        resultado.PreguntaId,
                        resultado.Acierto,
                        resultado.Fecha
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        /* PUT y DELETE
        //
        // Desde un punto de vista de integridad de datos
        // no tiene mucho sentido permitir editar resultados de preguntas
        //
        // PUT: api/Resultado/Editar
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Resultado resultado)
        {
            try
            {
                var resultadoExistente = _dbcontext.Resultados.Find(resultado.Id);

                if (resultadoExistente == null)
                {
                    return NotFound(new { mensaje = "Resultado no encontrado" });
                }

                // Actualizar solo los campos modificables
                resultadoExistente.Acierto = resultado.Acierto;
                resultadoExistente.Fecha = resultado.Fecha ?? resultadoExistente.Fecha;

                _dbcontext.Resultados.Update(resultadoExistente);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        

        // DELETE: api/Resultado/Eliminar/5
        [HttpDelete]
        [Route("Eliminar/{idResultado:int}")]
        public IActionResult Eliminar(int idResultado)
        {
            try
            {
                var resultado = _dbcontext.Resultados.Find(idResultado);

                if (resultado == null)
                {
                    return NotFound(new { mensaje = "Resultado no encontrado" });
                }

                _dbcontext.Resultados.Remove(resultado);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Resultado eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        */
    }
}