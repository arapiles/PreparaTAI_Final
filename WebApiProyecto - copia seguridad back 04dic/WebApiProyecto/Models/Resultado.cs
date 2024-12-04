using System;
using System.Collections.Generic;

namespace WebApiProyecto.Models;

public partial class Resultado
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int PreguntaId { get; set; }

    public bool Acierto { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Pregunta Pregunta { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
