<script setup>
import servicioUsuario from "../../services/servicioUsuario.js";
// Importar componentes Modales
import DetallesUsuarioModal from '../../components/modals/DetallesUsuarioModal.vue';
import EditarUsuarioModal from '../../components/modals/EditarUsuarioModal.vue';
import NuevoUsuarioModal from '../../components/modals/NuevoUsuarioModal.vue';

import { ref, onMounted, computed } from "vue";
import Swal from "sweetalert2";

let usuarios = ref(null);

// Interruptor para mostrar modal nuevo usuario
const mostrarNuevoUsuario = ref(false);

// Guarda el usuario que se ha seleccionado, se usa para mostrar el modal cuando clickas en ver detalles
const usuarioSeleccionado = ref(null);

// Estado para el modal de edición y el usuario a editar
const mostrarModalEditar = ref(false);

// Guarda copia de usuario seleccionado para el modal de edición
const usuarioEditar = ref(null);


// GET listado de usuarios
async function obtenerUsuarios() {
  try {
    //Obtener respuesta del servidor
    const response = await servicioUsuario.getAll();
    
    // Actualizar estado con los usuarios recibidos
    usuarios.value = response.data.response;
    
    //console.log('respuesta servidor:', response.data.response);
    //console.log('usuarios cargados:', usuarios.value);
    
    mensajeOK("Usuarios cargados correctamente");

  } catch (error) {
    console.error('Error al obtener usuarios:', error);
    mensajeError("Error al cargar los usuarios");
  }
}

// Funciones para abrir los modales
// Mostrar detalles de usuario
const mostrarDetalles = (usuario) => {
  usuarioSeleccionado.value = usuario;
  //console.log("usuarioSeleccionado: ", usuarioSeleccionado.value);
};

// Editar usuario
const editarUsuario = (usuario) => {
  usuarioEditar.value = { ...usuario }; // Copia para no modificar el original directamente
  mostrarModalEditar.value = true;
};

// DELETE usuario
async function borrarUsuario(usuario) {

  try {
    // Ventana confirm personalizada
    const result = await Swal.fire({
      title: "¿Está seguro?",
      text: "Esta acción eliminará el usuario y todos sus resultados asociados",
      color:"var(--color-primario)",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Sí, eliminar",
      cancelButtonText: "Cancelar",
      background: 'rgba(229, 229, 229, 0.9)', // Fondo gris semitransparente

    });

    // Si el usuario confirma
    if (result.isConfirmed) {
      // Eliminar usuario
      await servicioUsuario.delete(usuario.Id);

      // Actualizar lista usuarios en el front
      usuarios.value = usuarios.value.filter((u) => u.Id !== usuario.Id);

      mensajeOK("Usuario eliminado correctamente");
    }
  } catch (error) {
    mensajeError("Error al eliminar el usuario");
    console.error('Error:', error);
  }
}

// PUT actualizar usuario //////////////////////////////////////////////
const manejarEdicion = async (usuarioActualizado) => {

  try {
    // Actualizar usuario en la base de datos
    await servicioUsuario.update(usuarioActualizado);

    await obtenerUsuarios(); // Recargar lista usuarios

    mostrarModalEditar.value = false; // Cerrar el modal
    mensajeOK("Usuario actualizado correctamente");

  } catch (error) {
    
    mensajeError("Error al actualizar el usuario");
    console.error('Error completo:', error);
    console.error('Datos que causaron el error:', usuarioActualizado);
  }
};

// POST nuevo usuario //////////////////////////////////////////////
const manejarGuardado = async (usuarioNuevo) => {

  try {
    await servicioUsuario.register(usuarioNuevo); // Crear usuario en la base de datos

    await obtenerUsuarios(); // Recargar lista usuarios

    mostrarNuevoUsuario.value = false; // Cerrar el modal

    mensajeOK("Usuario creado correctamente");

  } catch (error) {
    mensajeError("Error al crear el usuario");
    console.error('Error:', error);
  }
};

onMounted(() => {
  obtenerUsuarios();
});

// Funciones mensajes feedback 
function mensajeError(mensaje) {
  Swal.fire({
    title: "Error",
    color:"var(--color-primario)",
    text: mensaje,
    icon: "error",
    confirmButtonColor: "var(--color-secundario)",

  });
}

function mensajeOK(mensaje) {
  Swal.fire({
    title: "¡Éxito!",
    color:"var(--color-primario)",
    text: mensaje,
    icon: "success",
    confirmButtonColor: "var(--color-secundario)",

  });
}

// Formatear la fecha
function formatearFecha(fecha) {
  return new Date(fecha).toLocaleDateString('es-ES', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });
}

// Traducir tipo de usuario
function getTipoUsuario(tipo) {
  switch (tipo) {
    case 1: return "Administrador";
    case 2: return "Usuario";
    default: return "Desconocido";
  }
}

// BÚSQUEDA //////////////////////////////////////////////
const busqueda = ref('');

const usuariosFiltrados = computed(() => { 
  if (!usuarios.value) return [];
  
  return usuarios.value.filter(usuario => {
    const terminoBusqueda = busqueda.value.toLowerCase().trim();
    
    // Si no hay término de búsqueda, devuelve todos los usuarios
    if (!terminoBusqueda) return true;
    
    // Busca por ID o Nombre o Email
    return usuario.Id.toString().includes(terminoBusqueda) ||
           usuario.Nombre.toLowerCase().includes(terminoBusqueda) ||
           usuario.Email.toLowerCase().includes(terminoBusqueda);
  });
});

// Función para limpiar la búsqueda
const limpiarBusqueda = () => {
  busqueda.value = '';
  paginaActual.value = 1;
};

// PAGINACIÓN /////////////////////////////////////////
const paginaActual = ref(1);
const itemsPorPagina = ref(5);

const usuariosPaginados = computed(() => {
  if (!usuarios.value) return []; 

  const inicio = (paginaActual.value - 1) * itemsPorPagina.value;
  const fin = inicio + itemsPorPagina.value;
  
  return usuariosFiltrados.value.slice(inicio, fin);
});

// Calcular el  total de pags
const paginasTotales = computed(() => {
  return Math.ceil(usuariosFiltrados.value.length / itemsPorPagina.value);
});
//////////////////////////////////////////////////////////



</script>

<template>
  <div class="contenedor-admin">
    <!-- Cabecera con título y botón -->
    <div class="cabecera">
      <h1>Gestión de Usuarios</h1>
    </div>

    <!-- Barra de herramientas -->
    <div class="barra-herramientas">
      <button class="btn-nuevoUsuario" @click="mostrarNuevoUsuario = true">
        Nuevo Usuario
      </button>

      <!-- BUSCAR USUARIO -->
       
      <div class="buscadorUsuario">
        <form class="formulario-busqueda">
        <input 
          type="text" 
          v-model="busqueda" 
          placeholder="Buscar por ID, nombre o email..." 
          class="campo-busqueda"
        >
        <button 
          v-if="busqueda" 
          @click="limpiarBusqueda" 
          class="boton-limpiar"
          title="Limpiar búsqueda"
        >
          ×
        </button>
      </form>
      </div>

      
    </div>

    <!-- Tabla de usuarios -->
    <div class="contenedor-tabla">
      <table class="tabla-usuarios">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Email</th>
            <th>Tipo Usuario</th>
            <th>Fecha Registro</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="usuario in usuariosPaginados" :key="usuario.Id">
            <td class="celda-id">{{ usuario.Id }}</td>
            <td class="celda-nombre">{{ usuario.Nombre }}</td>
            <td class="celda-email">{{ usuario.Email }}</td>
            <td class="celda-tipo">{{ getTipoUsuario(usuario.TipoUsuario) }}</td>
            <td class="celda-fecha">{{ formatearFecha(usuario.FechaRegistro) }}</td>
            <td class="celda-acciones">
              <button class="boton-detalles" @click="mostrarDetalles(usuario)" title="Ver detalles">
                👁️
              </button>
              <button class="boton-editar" @click="editarUsuario(usuario)" title="Editar usuario">
                ✏️
              </button>
              <button class="boton-eliminar" @click="borrarUsuario(usuario)" title="Eliminar usuario">
                🗑️
              </button>
            </td>
          </tr>
        </tbody>
      </table>


      <!-- PAGINACIÖN -->
      <div class="paginacion">
        <button 
          @click="paginaActual--" 
          :disabled="paginaActual === 1" 
          class="boton-paginacion"
        >
          Anterior
        </button>

        <span>Página {{ paginaActual }} / {{ paginasTotales }}</span>

        <button 
          @click="paginaActual++"
          :disabled="paginaActual >= paginasTotales"  
          class="boton-paginacion"
        >
          Siguiente
        </button>

      </div>

    </div>

    <!-- MODALES -->
    <!-- Modal Detalles -->
    <DetallesUsuarioModal v-if="usuarioSeleccionado" :usuario="usuarioSeleccionado"
      @close="usuarioSeleccionado = null" />

    <!-- Modal Editar -->
    <EditarUsuarioModal v-if="mostrarModalEditar" :usuario="usuarioEditar" @close="mostrarModalEditar = false"
      @save="manejarEdicion" />

    <!-- Modal Nuevo -->
    <NuevoUsuarioModal v-if="mostrarNuevoUsuario" @close="mostrarNuevoUsuario = false" @save="manejarGuardado" />

  </div>
</template>

<style scoped>
.contenedor-admin {
  padding: 2rem;
  padding-top: 3rem;
  padding-bottom: 6rem;
  max-width: 80%;
  margin: 0 auto;
  margin-top: 30px;
  margin-bottom: 30px;
  border-radius: 8px;
  background: var(--color-fondo2);
}

.cabecera {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
}

h1{
  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 10px;
  text-decoration-color: var(--color-resaltar);
}

.btn-nuevoUsuario {
  background-color: var(--color-primario);
  color: white;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  transition: background-color 0.3s ease;
  width: auto;
}

.btn-nuevoUsuario:hover {
  background-color: #3b5a98;
}

.barra-herramientas {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
}

.campo-busqueda {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  width: 300px;
  font-size: 1rem;
}



.selector {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
}

.contenedor-tabla {
  
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.tabla-usuarios {
  width: 100%;
  border-collapse: collapse;
  background-color: white;
  color: var(--color-texto-secundario);
}

th {
  background-color: var(--color-terciario);
  color: var(--color-primario);
  padding: 1rem;
  text-align: left;
  font-weight: bold;
}

td {
  color: var(--color-texto-secundario);

  padding: 1rem;
  border-bottom: 1px solid #eee;
}

.celda-acciones {
  display: flex;
  gap: 0.5rem;
}

.boton-detalles,
.boton-editar,
.boton-eliminar {
  padding: 0.5rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.3s ease;
  width: auto;
}

.boton-detalles {
  background-color: #3498db;
  color: white;
}

.boton-detalles:hover {
  background-color: #2980b9;
}

.boton-editar {
  background-color: #f1c40f;
}

.boton-editar:hover {
  background-color: #f39c12;
}

.boton-eliminar {
  background-color: #e74c3c;
  color: white;
}

.boton-eliminar:hover {
  background-color: #c0392b;
}

/* Estilos específicos para las celdas */
.celda-email,
.celda-nombre {
  max-width: 200px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.celda-fecha {
  white-space: nowrap;
}

/* Estilos para filas alternas */
.tabla-usuarios tbody tr:nth-child(even) {
  background-color: #f8f9fa;
}

.tabla-usuarios tbody tr:hover {
  background-color: #e9ecef;
}

/* Paginación */
.paginacion {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  gap: 20px;
  align-items: center;
  color: var(--color-primario);
  padding: 10px;
}

.boton-paginacion {
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  background-color: var(--color-primario);
  color: white;
  cursor: pointer;
}

.boton-paginacion:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.buscadorUsuario {
  position: relative;
  flex: 1;
}

.campo-busqueda {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  width: 300px;
  font-size: 1rem;
  margin-right: 18px;
}

.boton-limpiar {
  background-color: #eebfbf;

border: none;
font-size: 1.5rem;
color: #ffffff;
cursor: pointer;
border-radius: 50%;
width: 30px;
height: 28px;

transition: all 0.2s ease;
}

.boton-limpiar:hover {
  background-color: #f98383;
  color: #ffffff;
}

/* Diseño responsivo */
@media (max-width: 768px) {
  .contenedor-admin {
    margin-top: 8rem;
    padding: 1rem;
  }

  .barra-herramientas {
    flex-direction: column;
  }

  .campo-busqueda,
  .selector {
    width: 100%;
  }

  .tabla-usuarios {
    display: block;
    overflow-x: auto;
  }
  .buscadorUsuario {
    width: 100%;
  }
  
  .campo-busqueda {
    width: 100%;
  }
  
  .boton-limpiar {
    right: 10px;
  }

   /* Ocultar columnas mv */
   .celda-email,
  th:nth-child(3) {  /* Columna del email*/
    display: none;
  }

  .celda-fecha,
  th:nth-child(5) {  /* Columna de fecha*/
    display: none;
  }

  .celda-tipo,
  th:nth-child(4) {  /* Columna de tipo*/
    width: 4rem;      
  max-width: 6.5rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  }

  .btn-nuevoUsuario{
    align-self: flex-start;
    
  }
}
</style>