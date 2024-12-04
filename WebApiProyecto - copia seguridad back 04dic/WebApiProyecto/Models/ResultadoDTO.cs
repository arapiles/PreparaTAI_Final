using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class ResultadoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PreguntaId { get; set; }
        public bool Acierto { get; set; }
        public DateTime? Fecha { get; set; }

        // Información relevante de la pregunta
        public string TextoPregunta { get; set; }
        public string Bloque { get; set; }
        public string Tema { get; set; }

        // Información básica del usuario
        public string NombreUsuario { get; set; }
    }
}
