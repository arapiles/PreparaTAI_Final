using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class UsuarioEditarDTO
    {
        [Required(ErrorMessage = "El Id es requerido")]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string? Email { get; set; }

        public byte? TipoUsuario { get; set; }
    }
}