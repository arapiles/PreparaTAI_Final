using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class UsuarioCambioContraseñaDTO
    {
        [Required(ErrorMessage = "El ID de usuario es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La contraseña actual es requerida")]
        public string ContraseñaActual { get; set; } = null!;

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string NuevaContraseña { get; set; } = null!;
    }
}