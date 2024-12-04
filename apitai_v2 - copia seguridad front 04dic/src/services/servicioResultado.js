import http from "./http-axios";

class servicioResultado {
    // GET lista todos los resultados
    getLista() {
        return http.get('/api/Resultado/Lista');
    }

    // GET obtener un resultado específico
    get(id) {
        return http.get(`/api/Resultado/Obtener/${id}`);
    }

    // GET resultados de un usuario específico
    getResultadosUsuario(idUsuario) {
        return http.get(`/api/Resultado/ResultadosUsuario/${idUsuario}`);
    }

    // GET estadísticas por bloques de un usuario
    getEstadisticasBloques(idUsuario) {
        return http.get(`/api/Resultado/EstadisticasBloques/${idUsuario}`);
    }

    // GET estadísticas por temas de un usuario
    getEstadisticasTemas(idUsuario) {
        return http.get(`/api/Resultado/EstadisticasTemas/${idUsuario}`);
    }

    // POST guardar un resultado
    guardar(resultado) {
        return http.post('/api/Resultado/Guardar', {
            UsuarioId: resultado.UsuarioId,
            PreguntaId: resultado.PreguntaId,
            Acierto: resultado.Acierto
        });
    }
}

export default new servicioResultado();