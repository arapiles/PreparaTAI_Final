<script setup>
import { RouterLink, useRouter } from 'vue-router';
import servicioUsuario from '../services/servicioUsuario';
import { User } from 'lucide-vue-next'; // importar icono de usuario
import logo from './logo.vue';

const router = useRouter();
// Usar directamente el estado reactivo compartido
const usuario = servicioUsuario.usuario;

// Definir menús por rol
const menus = {
  invitado: ['Inicio', 'Tai', 'Nosotros', 'Contacto', 'Login'],
  usuario: ['Examen', 'Resultados'],
  admin: ['Dashboard', 'Preguntas', 'Usuarios']
};

const cerrarSesion = async () => {
  servicioUsuario.cerrarSesion();
  router.push('/');
};
</script>

<template>
  <nav class="menu">
    <!-- Logo -->
    <logo />
    <!-- Menú invitados -->
    <template v-if="!usuario">

      <RouterLink v-for="item in menus.invitado" :key="item" :to="{ name: item }" class="menu-link">
        {{ item }}
      </RouterLink>

    </template>

    <!-- Menú admin -->
    <template v-if="usuario?.TipoUsuario === 1"> <!-- usuario && usuario.TipoUsuario === 1 -->
      <RouterLink v-for="item in menus.admin" :key="item" :to="{ name: item }" class="menu-link">
        {{ item }}
      </RouterLink>
    </template>

    <!-- Menú usuarios normales -->
    <template v-if="usuario?.TipoUsuario === 2">
      <RouterLink v-for="item in menus.usuario" :key="item" :to="{ name: item }" class="menu-link">
        {{ item }}
      </RouterLink>
    </template>

    <!-- Nombre usuario y cerrar sesión -->
    <div v-if="usuario" class="usuario-menu">
      <User color="white" size="18" />
      <span class="usuario-name">{{ usuario.Nombre }}</span>
      <button @click="cerrarSesion" class="logout-btn">
        Cerrar Sesión
      </button>
    </div>
  </nav>
</template>

<style scoped>
/* Prubas Nesting CSS */
.menu {
  width: 100%;
  display: flex;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 10000;
  background: var(--color-primario);
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  padding: 0 20px;
  margin-bottom: 200px;
}

/* Enlaces del menú */
.menu-link {
  color: var(--color-fondo);
  text-decoration: none;
  padding: 15px 20px;
  padding-top: 19px;
  border-bottom: 3px solid transparent;
  transition: all 0.3s ease;
  font-weight: bold;
}

.menu-link:hover {
  color: var(--color-primario);
  background-color: var(--color-resaltar2);
  border-bottom: 3px solid var(--color-secundario);
}


/* Menú de usuario */
.usuario-menu {
  margin-left: auto;
  display: flex;
  align-items: center;
  gap: 15px;
}

.usuario-name {
  color: white;
  font-weight: bold;
  margin-right: 5px;
}

.logout-btn {
  padding: 8px 16px;
  background-color: rgba(0, 0, 0, 0.2);
  border: 1px solid white;
  color: white;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.logout-btn:hover {
  background-color: rgba(186, 1, 1, 0.669);
}



/* Login a la derecha */
a.menu-link[href="/login"] {

  margin-left: auto;
}



/* Media queries mv y tablet*/
@media (max-width: 768px) {
  .logo {
    display: none;
  }

  .menu {
    display: flex;
    flex-direction: column;
    position: absolute;
    width: 100%;
    padding-top: 0.5rem;
    padding-bottom: 1rem;

  }

  .menu-link {
    padding: 0.2rem;
    text-align: center;
    flex: 2;


  }

  .usuario-menu {
    width: 100%;
    justify-content: center;
    margin-top: 1rem;
  }

}

/* Implementar menu hamburguesa */
</style>