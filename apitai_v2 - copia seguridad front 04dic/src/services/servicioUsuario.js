import http from "./http-axios";
import { ref } from 'vue';

// Estado reactivo compartido
const usuarioActual = ref(null);

// Inicializar con datos del localStorage si existen
const datosGuardados = localStorage.getItem('datosUsuario');
if (datosGuardados) {
    usuarioActual.value = JSON.parse(datosGuardados);
}

class servicioUsuario {
    // Get para el estado reactivo compartido
    get usuario() {
        return usuarioActual;
    }

    // GET listado usuarios
    getAll() {
        return http.get('/api/Usuario/Lista');
    }

    // GET usuario por Id
    get(Id) {
        return http.get(`/api/Usuario/Obtener/${Id}`);
    }

    // GET usuario por Email
    getByEmail(Email) {
        return http.get(`/api/Usuario/ObtenerPorEmail/${Email}`);
    }

    // GET Ranking [Pendiente de implementar]
    getRanking() {
        return http.get('/api/Usuario/RankingGlobal');
    }

    // POST Login
    login(data) {
        return http.post('/api/Usuario/Login', {
            Email: data.Email,
            Contraseña: data.Contraseña
            
        }).then(response => {
            // Actualizar el estado reactivo compartido
            usuarioActual.value = response.data.response;
            this.guardarSesion(response.data.response);
            return response;
        });
    }

    // POST Registro [Admin registra usuario previo pago desde AdminUsuarios]
    register(data) {
        return http.post('/api/Usuario/Registro', {
            Nombre: data.Nombre,
            Email: data.Email,
            Contraseña: data.Contraseña,
            TipoUsuario: data.TipoUsuario
        });
    }

    // PUT Editar usuario
    update(data) {
        return http.put('/api/Usuario/Editar', {
            Id: data.Id,
            Nombre: data.Nombre,
            Email: data.Email,
            TipoUsuario: data.TipoUsuario
        }).then(response => {
            // Actualizar estado reactivo si es el usuario actual
            if (usuarioActual.value && usuarioActual.value.Id === data.Id) {
                usuarioActual.value = { ...usuarioActual.value, ...data };
                this.guardarSesion(usuarioActual.value);
            }
            return response;
        });
    }

    // PUT Cambiar Contraseña
    changePassword(data) {
        return http.put('/api/Usuario/CambiarContraseña', {
            Id: data.Id,
            ContraseñaActual: data.ContraseñaActual,
            NuevaContraseña: data.NuevaContraseña
        });
    }

    // DELETE Eliminar usuario
    delete(Id) {
        return http.delete(`/api/Usuario/Eliminar/${Id}`);
    }

    // Gestión de sesión
    guardarSesion(datosUsuario) {
        if (!datosUsuario) {
            console.error('Intentando guardar datos de usuario nulos');
            return;
        }
        console.log('Guardando sesión:', datosUsuario);
        localStorage.setItem('datosUsuario', JSON.stringify(datosUsuario));
        usuarioActual.value = datosUsuario;
    }

    obtenerSesion() {
        if (!usuarioActual.value) {
            const datosString = localStorage.getItem('datosUsuario');
            if (datosString) {
                try {
                    usuarioActual.value = JSON.parse(datosString);
                    console.log('Datos de sesión recuperados:', usuarioActual.value);
                } catch (error) {
                    console.error('Error al obtener sesión:', error);
                }
            }
        }
        return usuarioActual.value;
    }

    cerrarSesion() {
        console.log('Cerrando sesión');
        localStorage.removeItem('datosUsuario');
        usuarioActual.value = null;
    }

    estaAutenticado() {
        return usuarioActual.value !== null;
    }
}

export default new servicioUsuario();