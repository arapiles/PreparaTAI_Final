﻿using System.ComponentModel.DataAnnotations;

namespace WebApiProyecto.Models
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Contraseña { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de usuario es requerido")]
        public byte TipoUsuario { get; set; }
    }
}