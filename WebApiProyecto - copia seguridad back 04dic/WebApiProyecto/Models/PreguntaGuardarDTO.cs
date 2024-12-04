using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class PreguntaGuardarDTO
    {
        [Required(ErrorMessage = "La pregunta es requerida")]
        public string Pregunta { get; set; } = null!;

        [Required(ErrorMessage = "La opción 1 es requerida")]
        public string Opcion1 { get; set; } = null!;

        [Required(ErrorMessage = "La opción 2 es requerida")]
        public string Opcion2 { get; set; } = null!;

        [Required(ErrorMessage = "La opción 3 es requerida")]
        public string Opcion3 { get; set; } = null!;

        [Required(ErrorMessage = "La opción 4 es requerida")]
        public string Opcion4 { get; set; } = null!;

        [Required(ErrorMessage = "La respuesta es requerida")]
        public string Respuesta { get; set; } = null!;

        [Required(ErrorMessage = "El bloque es requerido")]
        public string Bloque { get; set; } = null!;

        [Required(ErrorMessage = "El tema es requerido")]
        public string Tema { get; set; } = null!;

        [Required(ErrorMessage = "El origen es requerido")]
        public string Origen { get; set; } = null!;
    }
}
