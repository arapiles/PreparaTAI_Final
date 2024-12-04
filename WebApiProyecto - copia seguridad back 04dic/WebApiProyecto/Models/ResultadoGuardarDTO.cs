using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class ResultadoGuardarDTO
    {
        [Required(ErrorMessage = "El ID del usuario es requerido")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El ID de la pregunta es requerida")]
        public int PreguntaId { get; set; }

        [Required(ErrorMessage = "Debe indicar si la respuesta es correcta o no")]
        public bool Acierto { get; set; }
    }
}