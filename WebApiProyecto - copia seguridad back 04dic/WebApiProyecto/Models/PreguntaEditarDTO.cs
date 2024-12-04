using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    // Simplifica el JSON que le llega al front en el PUT y añade validaciones
    public class PreguntaEditarDTO
    {
        [Required(ErrorMessage = "El ID es requerido")]
        public int Id { get; set; }

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
// null! es un operador de supresión de advertencias de nulabilidad. Le dice al compilador sé que esto podría ser null, pero confía en mí se usa porque es required y evita advertencias