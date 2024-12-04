<script setup>
import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import servicioUsuario from '../services/servicioUsuario';
import Swal from 'sweetalert2';

const router = useRouter();

// Login form
const email = ref('');
const contraseña = ref('');

// Almacena datos usuario logueado
const usuario = ref(null);

// Detectar cambios en el usuario
watch(usuario, (nuevoUsuario) => {
  if (nuevoUsuario) {
    
    // Redirigir según el tipo de usuario
    if (nuevoUsuario.TipoUsuario === 1) {
      router.push('/control_panel');

    } else {
      router.push('/resultados');
    }
  }
});

async function manejoLogin() {
  if (!email.value || !contraseña.value) {
    Swal.fire({
      icon: 'warning',
      title: 'Campos incompletos',
      text: 'Por favor, complete todos los campos',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: 'var(--color-principal)',
      confirmButtonColor: '#780494'
    });
    return;
  }

  try {
    const response = await servicioUsuario.login({
      Email: email.value,
      Contraseña: contraseña.value
    });

    const usuarioData = response.data.response;
    //servicioUsuario.guardarSesion(usuarioData);
    
    // Actualizar el estado del usuario que dispara el watch
    //usuario.value = usuarioData;

    await Swal.fire({
      icon: 'success',
      title: `¡Bienvenido ${usuarioData.Nombre}!`, // Usar usuarioData.Nombre
      timer: 1500,
      showConfirmButton: false,
      background: '#e5e5e5',
      color: 'var(--color-secundario)',
      iconColor: 'var(--color-secundario)'
    });

    // Redireccionar según tipo de usuario
    if (usuarioData.TipoUsuario === 1) {
            router.push('/control_panel');
        } else {
            router.push('/resultados');
        }

  } catch (error) {
    console.error('Error al iniciar sesión: ', error);
    Swal.fire({
      icon: 'error',
      title: 'Error de acceso',
      text: 'Error al iniciar sesión',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
      confirmButtonColor: '#780494'
    });
  }
}
</script>

<template>
  <div class="login-container">
    <div class="login-box">
      <h2>Iniciar Sesión</h2>

      <form @submit.prevent="manejoLogin">
        <div class="form-group">
          <label for="email">Email:</label>
          <input id="email" type="email" v-model="email" required placeholder="Ingrese su email" />
        </div>

        <div class="form-group">
          <label for="password">Contraseña:</label>
          <input id="password" type="password" v-model="contraseña" required placeholder="Ingrese su contraseña" />
        </div>

        <button type="submit" class="btn-login">
          Iniciar Sesión
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped>
body{
  background-color: black;
}
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: linear-gradient(45deg, var(--color-terciario), var(--color-resaltar2));
  padding: 20px;
}

.login-box {
  background:rgba(255, 255, 255, 0.122);
  padding: 2rem;
  border-radius: 8px;
  border: 2px solid rgba(255, 255, 255, .2);
  width: 100%;
  max-width: 400px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

h2 {
  text-align: center;
  font-weight: bold;
  font-size:x-large;
  color: var(--color-primario);
  margin-bottom: 2rem;

  text-decoration: underline;
  text-underline-offset: 5px;
  text-decoration-color: var(--color-secundario);
}

.form-group {
  margin-bottom: 1.5rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  color: var(--color-primario);
  font-weight: bold;
}

input {
  width: 100%;
  padding: 0.75rem;
  
  border: 1px solid rgba(255, 255, 255, .2);

  border-radius: 30px;
  font-size: 1rem;
}

input:focus {
  outline: none;
  border-color: var(--color-resaltar2);
}

.btn-login {
  width: 100%;
  padding: 0.75rem;
  background: var(--color-primario);
  color: var(--color-fondo);
  border: none;
  border-radius: 30px;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;

}

.btn-login:hover {
  background: var(--color-resaltar);
  color: var(--color-primario);
}
</style>