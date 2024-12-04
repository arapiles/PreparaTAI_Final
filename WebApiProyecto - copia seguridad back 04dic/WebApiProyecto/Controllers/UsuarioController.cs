using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProyecto.Models;
using WebApiProyecto.Extensions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cors;


namespace WebApiProyecto.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApitaiContext _dbcontext;

        public UsuarioController(ApitaiContext context)
        {
            _dbcontext = context;
        }
    
        // GET: api/Usuario/Lista
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            try
            {
                var lista = _dbcontext.Usuarios
                    .Select(u => new {
                        u.Id,
                        u.Nombre,
                        u.Email,
                        u.TipoUsuario,
                        u.FechaRegistro,
                        ResultadosCount = u.Resultados.Count
                    })
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        
        // GET: api/Usuario/Obtener/5
        [HttpGet]
        [Route("Obtener/{idUsuario:int}")]
        public IActionResult Obtener(int idUsuario)
        {
            try
            {
                var usuario = _dbcontext.Usuarios
                    .Select(u => new {
                        u.Id,
                        u.Nombre,
                        u.Email,
                        u.TipoUsuario, // Admin = 1 , Usuario = 2
                        u.FechaRegistro,
                        ResultadosCount = u.Resultados.Count, // Conteo de resultados
                    })
                    .FirstOrDefault(u => u.Id == idUsuario);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        
        // GET: api/Usuario/ObtenerPorEmail/usuario@ejemplo.com
        [HttpGet]
        [Route("ObtenerPorEmail/{email}")]
        public IActionResult ObtenerPorEmail(string email)
        {
            try
            {
                var usuario = _dbcontext.Usuarios
                    .Select(u => new {
                        u.Id,
                        u.Nombre,
                        u.Email,
                        u.TipoUsuario,
                        u.FechaRegistro
                    })
                    .FirstOrDefault(u => u.Email == email);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Usuario/RankingGlobal
        [HttpGet]
        [Route("RankingGlobal")]
        public IActionResult ObtenerRankingGlobal()
        {
            try
            {
                var ranking = _dbcontext.Usuarios
                    .Where(u => u.Resultados.Any()) // Solo usuarios con resultados
                    .Select(u => new
                    {
                        u.Id,
                        u.Nombre,
                        TotalPreguntas = u.Resultados.Count,
                        PorcentajeAciertos = Math.Round((double)u.Resultados.Count(r => r.Acierto) / u.Resultados.Count * 100, 2),
                        UltimaActividad = u.Resultados.Max(r => r.Fecha)
                    })
                    .OrderByDescending(x => x.PorcentajeAciertos)
                    .ThenByDescending(x => x.TotalPreguntas)
                    .Take(10)
                    .ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ranking });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }


        // POST: api/Usuario/Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO login)
        {
            try
            {
                var usuario = _dbcontext.Usuarios
                    .FirstOrDefault(u => u.Email == login.Email);

                if (usuario == null || !VerifyPassword(login.Contraseña, usuario.Contraseña))
                {
                    return BadRequest(new { mensaje = "Email o contraseña incorrectos" });
                }

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "ok",
                    response = new
                    {
                        usuario.Id,
                        usuario.Nombre,
                        usuario.Email,
                        usuario.TipoUsuario
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // POST: api/Usuario/Registro
        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro([FromBody] UsuarioRegistroDTO modelo)
        {
            try
            {
                // Verificar si el email ya existe
                if (_dbcontext.Usuarios.Any(u => u.Email == modelo.Email))
                {
                    return BadRequest(new { mensaje = "El email ya está registrado" });
                }

                // Crear nuevo usuario
                var usuario = new Usuario
                {
                    Nombre = modelo.Nombre,
                    Email = modelo.Email,
                    Contraseña = HashPassword(modelo.Contraseña),
                    TipoUsuario = modelo.TipoUsuario,
                    FechaRegistro = DateTime.Now
                };

                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new
                {
                    mensaje = "Usuario registrado correctamente",
                    response = new
                    {
                        usuario.Id,
                        usuario.Nombre,
                        usuario.Email,
                        usuario.TipoUsuario,
                        usuario.FechaRegistro
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // PUT: api/Usuario/Editar
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] UsuarioEditarDTO modelo)
        {
            try
            {
                var usuario = _dbcontext.Usuarios.Find(modelo.Id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                // Verificar si se está intentando cambiar el email y si ya existe
                if (modelo.Email != usuario.Email &&
                    _dbcontext.Usuarios.Any(u => u.Email == modelo.Email))
                {
                    return BadRequest(new { mensaje = "El email ya está en uso" });
                }

                // Actualizar campos
                usuario.Nombre = modelo.Nombre ?? usuario.Nombre;
                usuario.Email = modelo.Email ?? usuario.Email;
                usuario.TipoUsuario = modelo.TipoUsuario ?? usuario.TipoUsuario;

                _dbcontext.Usuarios.Update(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "Usuario actualizado correctamente",
                    response = new
                    {
                        usuario.Id,
                        usuario.Nombre,
                        usuario.Email,
                        usuario.TipoUsuario,
                        usuario.FechaRegistro
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // PUT: api/Usuario/CambiarContraseña
        [HttpPut]
        [Route("CambiarContraseña")]
        public IActionResult CambiarContraseña([FromBody] UsuarioCambioContraseñaDTO modelo)
        {
            try
            {
                var usuario = _dbcontext.Usuarios.Find(modelo.Id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                // Verificar contraseña actual
                if (!VerifyPassword(modelo.ContraseñaActual, usuario.Contraseña))
                {
                    return BadRequest(new { mensaje = "La contraseña actual es incorrecta" });
                }

                usuario.Contraseña = HashPassword(modelo.NuevaContraseña);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Contraseña actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // DELETE: api/Usuario/Eliminar/5
        [HttpDelete]
        [Route("Eliminar/{idUsuario:int}")]
        public IActionResult Eliminar(int idUsuario)
        {
            try
            {
                var usuario = _dbcontext.Usuarios
                    .Include(u => u.Resultados)
                    .FirstOrDefault(u => u.Id == idUsuario);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "Usuario no encontrado" });
                }

                // Eliminar resultados asociados
                _dbcontext.Resultados.RemoveRange(usuario.Resultados);

                // Eliminar usuario
                _dbcontext.Usuarios.Remove(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario y sus resultados eliminados correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        // Método auxiliar para encriptar contraseñas
        /*
        private string GetSHA256(string str) // CAMBIAT por bcript!!!!!!!!!!
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        */

        // Método para hashear la contraseña
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método para verificar la contraseña
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}