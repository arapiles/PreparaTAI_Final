using System;
using System.Collections.Generic;

namespace WebApiProyecto.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public byte TipoUsuario { get; set; } // Admin = 1 , Usuario = 2

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
