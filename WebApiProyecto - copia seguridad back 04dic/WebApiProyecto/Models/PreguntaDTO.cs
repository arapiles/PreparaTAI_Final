namespace WebApiProyecto.Models
{
    // Simplifica el JSON que le llega al front
    public class PreguntaDTO
    {
        public int Id { get; set; }
        public string Pregunta { get; set; } // Cambió Pregunta1 por Pregunta
        public string Opcion1 { get; set; }
        public string Opcion2 { get; set; }
        public string Opcion3 { get; set; }
        public string Opcion4 { get; set; }
        public string Respuesta { get; set; }
        public string Bloque { get; set; }
        public string Tema { get; set; }
        public string Origen { get; set; }
    }
}

// Quitamos la colección Resultados que no nos hace falta