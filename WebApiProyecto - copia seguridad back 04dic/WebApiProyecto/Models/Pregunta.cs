using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApiProyecto.Models;

public partial class Pregunta
{
    public int Id { get; set; }

    public string Pregunta1 { get; set; } = null!; // los nombre de los miembros no pueden coincidir con sus tipos envolventes

    public string Opcion1 { get; set; } = null!;

    public string Opcion2 { get; set; } = null!;

    public string Opcion3 { get; set; } = null!;

    public string Opcion4 { get; set; } = null!;

    public string Respuesta { get; set; } = null!;

    public string Bloque { get; set; } = null!;

    public string Tema { get; set; } = null!;

    public string Origen { get; set; } = null!;

    //[JsonIgnore] Ya no hace falta, limpio el JSON con una DTO
    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
