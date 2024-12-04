<script setup>
import servicioPregunta from "../../services/servicioPregunta.js";
import NuevaPreguntaModal from '../../components/modals/NuevaPreguntaModal.vue';
import DetallesPreguntaModal from '../../components/modals/DetallesPreguntaModal.vue';
import EditarPreguntaModal from '../../components/modals/EditarPreguntaModal.vue';
import { ref, onMounted, reactive, computed } from "vue";
import Swal from "sweetalert2";


let preguntas = ref(null); // Almacena listado de preguntas

// Interruptor ara mostrar modal nueva pregutna en el template
const mostrarNuevaPregunta = ref(false); // al principio el modal esta oculto

// Almacena pregunta seleccionada para mostrar detalles
const preguntaSeleccionada = ref(null);

// Estado para el modal de edición y la pregunta a editar
const mostrarModalEditar = ref(false);
const preguntaEditar = ref(null);


// BUSQUEDA //////////////////////////////////////////////
const busqueda = ref(''); // Cuando el usuario escribe

const preguntasFiltradas = computed(() => { // computed reacciona automáticamente
  if (!preguntas.value) return []; 
  
  return preguntas.value.filter(pregunta => { // y filtra preguntas por termino, reaccionando automáticamente cuando cambia
    const terminoBusqueda = busqueda.value.toLowerCase().trim(); 
    
    if (!terminoBusqueda) return true; // Si no hay término de búsqueda, devuelve todas las preguntas
    
    return pregunta.Id.toString().includes(terminoBusqueda) ||
           pregunta.Pregunta.toLowerCase().includes(terminoBusqueda) ||
           pregunta.Tema.toLowerCase().includes(terminoBusqueda);
  });
});

// Función para limpiar la búsqueda
const limpiarBusqueda = () => {
  busqueda.value = '';
  paginaActual.value = 1; // Regresa a la primera página
};



// PAGINACIÓN /////////////////////////////////////////
const paginaActual = ref(1); // Guarda pagina actual
const itemsPorPagina = ref(5); // Numero de pregutnas por pagina

const preguntasPaginadas = computed(() => {

  if (!preguntas.value) {
    return [];
  }

  const inicio = (paginaActual.value - 1) * itemsPorPagina.value; // Calcula donde empieza la página

  const fin = inicio + itemsPorPagina.value; // Calcula donde termina

 return preguntasFiltradas.value.slice(inicio, fin); // y devuelve el subconjunto de preguntas para esta página
});

// Numero de paginas para la paginación
const paginasTotales = computed(() => {
  //if (!preguntas.value) return 0;
  //return Math.ceil(preguntas.value.length / itemsPorPagina.value); // Math.ceil redondea un decimal hacia arriba
  return Math.ceil(preguntasFiltradas.value.length / itemsPorPagina.value);

});
//////////////////////////////////////////////////////////


// GET listado de preguntas
async function obtenerPreguntas() { // La función se declara como async y se usa await para esperar la respuesta del servicio y se utiliza un bloque try/catch para manejar errores

  try {
    const response = await servicioPregunta.getAll();

    // Asignar los datos
    preguntas.value = response.data.response;
    
    //console.log('Preguntas cargadas:', preguntas.value);
    
    mensajeOK("Preguntas cargadas correctamente");

  } catch (error) {
    console.error('Error al obtener preguntas:', error);
    mensajeError(error.message || "Error al cargar las preguntas");
  }
}


// Funciones para abrir los modales
// MOSTRAR PREGUNTA 
const mostrarDetalles = (pregunta) => {
  preguntaSeleccionada.value = pregunta;
};

// Editar pregunta
const editarPregunta = (pregunta) => {
  preguntaEditar.value = { ...pregunta }; // Copia para no modificar la original directamente
  mostrarModalEditar.value = true;
};

// DELETE pregunta
async function borrarDatos(pregunta) {

  try {
    // Mensaje confirmación
    const resultado = await Swal.fire({
      title: '¿Está seguro?',
      text: "Esta acción eliminará la pregunta y sus resultados asociados",
      color:"var(--color-primario)",

      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',

      background: 'rgba(229, 229, 229, 0.9)', // Fondo gris semitransparente
    

    
    });

    // Si el usuario confirma
    if (resultado.isConfirmed) {
      // Eliminar la pregunta
      await servicioPregunta.delete(pregunta.Id);
      
      // Actualizar la lista de preguntas en el frontend
      preguntas.value = preguntas.value.filter((p) => p.Id !== pregunta.Id);
      
      mensajeOK("Pregunta eliminada correctamente");
    }
    
  } catch (error) {
    console.error('Error al eliminar la pregunta:', error);
    mensajeError("Error al eliminar la pregunta");
  }
}


// POST pregunta
const manejarGuardado = async (preguntaNueva) => {
  try {
    // Validación de campos requeridos
    if (!preguntaNueva.Pregunta || !preguntaNueva.Respuesta) {
      mensajeError("La pregunta y la respuesta son obligatorias");
      return;
    }

    // Realizar el POST
    await servicioPregunta.post(preguntaNueva);
    
    // Si el post es exitoso ctualiza  la lista y cerrar modal
    await obtenerPreguntas();
    mostrarNuevaPregunta.value = false;
    
    mensajeOK("Pregunta insertada correctamente");

  } catch (error) {
    console.error('Error al guardar la pregunta:', error);
    mensajeError("Error al añadir la pregunta");
  }
};


// PUT editar pregunta existente //
const manejarEdicion = async (preguntaActualizada) => {

  try {
    // Actualizar la pregunta en BBDD
    await servicioPregunta.update(preguntaActualizada.Id, preguntaActualizada); 

    await obtenerPreguntas(); // Recargar las preguntas

    mostrarModalEditar.value = false; // Cerrar modal
    mensajeOK("Pregunta actualizada correctamente");


  } catch (error) {
    
    mensajeError("Error al actualizar la pregunta");
    console.error('Error:', error);
  }
};

onMounted(() => {
  obtenerPreguntas();
});

function mensajeError(mensaje) {
  Swal.fire({
    title: "Oops...",
    color:"var(--color-primario)",

    text: mensaje,
    icon: "error",
    confirmButtonColor: "var(--color-secundario)",
    
  });
}

function mensajeOK(mensaje) {
  Swal.fire({
    title: "¡Genial!",
    color:"var(--color-primario)",
    text: mensaje,
    icon: "success",
    confirmButtonColor: "var(--color-secundario)",
  });
}

// IMPORTANTE: Implementar paginación, componetizar tabla
</script>

<template>
  <div class="contenedor-admin">
    <!-- Cabecera con título y botón -->
    <div class="cabecera">
      <h1>Gestión de Preguntas</h1>

    </div>

    <!-- Barra de búsqueda y filtros -->
    <div class="barra-herramientas">
      <button class="btn-nuevaPregunta" @click="mostrarNuevaPregunta = true">
        Nueva Pregunta
      </button>
      
      <div class="buscadorPregunta">
        <input type="text" v-model="busqueda" placeholder="Buscar por ID o texto..." class="campo-busqueda">
        <button 
        v-if="busqueda" 
        @click="limpiarBusqueda" 
        class="boton-limpiar"
        title="Limpiar búsqueda"
      >
        ×
      </button>
      </div>


      
    </div>

    <!-- TABLA -->
    <div class="contenedor-tabla">
      <table class="tabla-preguntas">
        <thead>
          <tr>
            <th>Id</th>
            <th>Pregunta</th>
            <th>Bloque</th>
            <th>Tema</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <!-- Recorre sobre preguntas ya paginadas -->
          <tr v-for="pregunta in preguntasPaginadas" :key="pregunta.Id">
            <td class="celda-id">{{ pregunta.Id }}</td>
            <td class="celda-pregunta">{{ pregunta.Pregunta }}</td>
            <td class="celda-bloque"> {{ pregunta.Bloque }}</td>
            <td class="celda-tema">{{ pregunta.Tema }}</td>
            <td class="celda-acciones">
              <button class="boton-detalles" @click="mostrarDetalles(pregunta)" title="Ver detalles">
                👁️
              </button>
              <button class="boton-editar" @click="editarPregunta(pregunta)" title="Editar pregunta">
                ✏️
              </button>
              <button class="boton-eliminar" @click="borrarDatos(pregunta)" title="Eliminar pregunta">
                🗑️
              </button>
            </td>
          </tr>
        </tbody>
      </table>

      <!-- PAGINACIÖN -->
      <div class="paginacion">
        <button @click="paginaActual--" :disabled="paginaActual === 1" class="boton-paginacion">
          Anterior
        </button>

        <span>Página {{ paginaActual }} / {{ paginasTotales }}</span>

        <button @click="paginaActual++"
          :disabled="!preguntas || paginaActual >= Math.ceil(preguntas.length / itemsPorPagina)" class="boton-paginacion">
          Siguiente
        </button> <!-- Math.ceil redondea un decimal hacia arriba para sacar el tocal de las paginas en entero -->
      </div>
    </div>

    <!-- MODALES -->
    <!-- Modal para nueva pregunta --> <!-- close y save son eventos personalizados definidos en el modal. Con close hacemos una version sencilla del @click y una funcion para cerrar -->
    <NuevaPreguntaModal 
      v-if="mostrarNuevaPregunta" 
      @close="mostrarNuevaPregunta = false"  
      @save="manejarGuardado" 
    />
    

    <!-- Modal para  detalles -->
    <DetallesPreguntaModal 
      v-if="preguntaSeleccionada" 
      :pregunta="preguntaSeleccionada"
      @close="preguntaSeleccionada = null" 
    />
  </div>

  <!-- Modal para editar -->
  <EditarPreguntaModal 
    v-if="mostrarModalEditar" 
    :pregunta="preguntaEditar" 
    @close="mostrarModalEditar = false"
    @save="manejarEdicion" 
  />

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

.btn-nuevaPregunta {
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

.btn-nuevaPregunta:hover {
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
  margin-right: 18px;
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

.tabla-preguntas {
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

.celda-pregunta {
  max-width: 300px;
}

.lista-opciones {
  list-style: none;
  padding: 0;
  margin: 0;
}

.lista-opciones li {
  margin-bottom: 0.5rem;
  color: #666;
}

.celda-acciones {
  display: flex;
  gap: 0.5rem;
}

.boton-editar,
.boton-eliminar {
  padding: 0.5rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.boton-editar {
  background-color: #ffd700;
}

.boton-eliminar {
  background-color: #dc3545;
  color: white;
}

.boton-editar:hover {
  background-color: #ffc107;
}

.boton-eliminar:hover {
  background-color: #c82333;
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

/* Mejoras en la tabla */
.tabla-preguntas {
  width: 100%;
  border-collapse: collapse;
  background-color: white;
}

.celda-id {

}

.celda-pregunta {
  max-width: 400px;

  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.celda-bloque {
  max-width: 400px;

  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.celda-tema {
  max-width: 400px;

  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;

}

/* Estilos filas alternas */
.tabla-preguntas tbody tr:nth-child(even) {
  background-color: #f8f9fa;
}

.tabla-preguntas tbody tr:hover {
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

  .celda-pregunta {
    max-width: none;
  }

  .tabla-preguntas {
    display: block;
    overflow-x: auto;
  }

   /* Ajsutar colunma mv */
   .celda-pregunta,
      th:nth-child(2) {  /* Columna  pregunta*/
        width: 5rem;      
        max-width: 12.1rem;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
     }

     /* Ocultar columnas mv */
   .celda-bloque,
    th:nth-child(3) {  /* Columna bloque*/
      display: none;
    }

    .celda-tema,
    th:nth-child(4) {  /* Columna tema*/
      display: none;
    }

  .btn-nuevaPregunta{
    align-self: flex-start;
    
  }
}
</style>