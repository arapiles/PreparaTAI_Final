import http from "./http-axios";

class servicioPregunta {
  // GET listado
  getAll() {
    return http.get('/api/Pregunta/ListaPreguntas');
  }

  // GET pregunta
  get(id) {
    return http.get(`/api/Pregunta/Obtener/${id}`);
  }

  // GET preguntas por bloque
  getPreguntasBloque(bloque) {
    return http.get(`/api/Pregunta/ObtenerPorBloque/${bloque}`);
  }

  // GET preguntas por tema
  getPreguntasTema(tema) {
    return http.get(`/api/Pregunta/ObtenerPorTema/${encodeURIComponent(tema)}`); // Soluciona el error de caracteres especiales en la url
  }

  // GET lista de bloques
  getListaBloques() {
    return http.get('/api/Pregunta/ListaBloques');
  }

  // GET lista de temas
  getListaTemas() {
    return http.get('/api/Pregunta/ListaTemas');
  }


  // POST pregunta
  post(pregunta) {
    return http.post('/api/Pregunta/Guardar', pregunta);
  }


  // DELETE pregunta
  delete(id) {
    return http.delete(`/api/Pregunta/Eliminar/${id}`);
  }
  

  // PUT editar pregunta
  update(id, data) {
    return http.put('/api/Pregunta/Editar', data);
  }


};

export default new servicioPregunta();