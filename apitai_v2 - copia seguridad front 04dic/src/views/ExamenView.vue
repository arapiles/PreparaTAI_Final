<script setup>
import { ref, onMounted, computed } from 'vue';
import servicioPregunta from '../services/servicioPregunta';
import servicioResultado from '../services/servicioResultado';
import servicioUsuario from '../services/servicioUsuario';
import ResultadosModal from '../components/modals/ResultadosModal.vue';
import TemporizadorEx from '../components/temporizadorEx.vue';
import Swal from 'sweetalert2';


// Estado para Selects, actuan de filtros 
const filtro = ref({
  cantidad: 5, 
  bloqueSeleccionado: '', 
  temaSeleccionado: '' 
  //origenSeleccionado:'' // Origen que mostrar examen 2022, 2019, chatgpt PENDIENTE IMPLEMENTAR EN BACKEND
});


// Estado para controlar exluir filtro no seleccionado
const modoSeleccion = ref(''); // O 'bloque' o 'tema'

// Estados para listas Selects
const preguntas = ref([]);
const bloques = ref([]);
const temas = ref([]);
//const origenes = ref([]); pendiete

// Estado almacena eleccion usuario
const respuestasUsuario = ref({});
// Estado para controlar si el examen fue corregido
const examenCorregido = ref(false);
// Estado para mostrar modal resultados
const mostrarResultados = ref(false);

// Recibe los resultados del examen para el modal
const resultados = ref({
  aciertos: 0,
  total: 0,
  nota: 0
});


onMounted(async () => {
  try {
    // Cargar datos iniciales
    await Promise.all([ // Se cargan simultanemente los bloques y los temas, el codigo esperara que ambas operaciones terminen antes de continuar
      cargarBloques(),
      cargarTemas()
      //cargarOrigenes() pendiente
    ]);


  } catch (error) {
    console.error('Error al cargar datos iniciales:', error);
    Swal.fire({
      icon: 'error',
      title: 'Error',
      text: 'Error al cargar los datos iniciales',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
    });
  }
});

// Manejador para cuando se selecciona modo bloque
const seleccionarModoBloque = () => {
  console.log('Seleccionando modo bloque');
  modoSeleccion.value = 'bloque';
  filtro.value.temaSeleccionado = ''; // Limpiar selección de tema
};

// Manejador para cuando se selecciona modo tema
const seleccionarModoTema = () => {
  console.log('Seleccionando modo tema');
  modoSeleccion.value = 'tema';
  filtro.value.bloqueSeleccionado = ''; // Limpiar selección de bloque
};


// Obtener bloques
async function cargarBloques() {
  try {
    const response = await servicioPregunta.getListaBloques();

    if (response && response.data && response.data.bloques) { // Si hay bloques en la respuesta
      bloques.value = response.data.bloques; // Guardar bloques en el estado
      console.log('Bloques cargados:', bloques.value);
    }

  } catch (error) {
    console.error('Error al cargar bloques:', error);
  }
}

// Obtener temas
async function cargarTemas() {
  try {
    const response = await servicioPregunta.getListaTemas();

    if (response && response.data && response.data.temas) {
      temas.value = response.data.temas;
      console.log('Temas cargados:', temas.value);
    }

  } catch (error) {
    console.error('Error al cargar temas:', error);
  }
}

// Obtener origenes FALTA BACKEND!!!!!!!!!!
/* async function cargarOrigenes() {
  try {
    const response = await servicioPregunta.getListaOrigenes();
    if (response.data?.origenes) {
      origenes.value = response.data.origenes;
      console.log('Origenes cargados:', origenes.value);
    }
  } catch (error) {
    console.error('Error al cargar origenes:', error);
  }
} */

// Cargar preguntas según la configuración
async function cargarPreguntas() {
  try {
    let response; // Respuesta de la API
    let preguntasData; // Guardar preguntas

    // Cargar por bloque
    if (modoSeleccion.value === 'bloque') {
      // Llamar al servicio para obtener preguntas por bloque
      response = await servicioPregunta.getPreguntasBloque(filtro.value.bloqueSeleccionado);
      // Guardar preguntas en el estado
      preguntasData = response.data.preguntas;
    }

    // Cargar por tema
    if (modoSeleccion.value === 'tema') {
      response = await servicioPregunta.getPreguntasTema(filtro.value.temaSeleccionado);
      preguntasData = response.data.preguntas;
    }

    // Validar si hay preguntas
    if (!preguntasData || !preguntasData.length) {
      await Swal.fire({
        icon: 'warning',
        title: 'Atención',
        text: 'No se encontraron preguntas',
        confirmButtonText: 'Aceptar',
        background: '#e5e5e5',
        color: '#770494d2',
      });
      return;
    }

    // Si todo está bien, procesar las preguntas
    preguntas.value = preguntasData
      .sort(() => Math.random() - 0.5) // Mezclar preguntas
      .slice(0, filtro.value.cantidad); // Y tomar solo la cantidad seleccionada en lugar de mostrar todas las preguntas obtenidas del servidor

    // Reiniciar estados asegura todo ok cuando se carguen nuevas pregntas
    respuestasUsuario.value = {};
    examenCorregido.value = false;

  } catch (error) {
    console.error('Error al cargar preguntas:', error);
    await Swal.fire({
      icon: 'error',
      title: 'Error',
      text: 'Error al cargar las preguntas',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
    });
  }
}


// Lo lanza btn NUEVO EXAMEN de configuración de preguntas 
function reiniciarExamen() {
  respuestasUsuario.value = {}; // limpia respuestas
  examenCorregido.value = false; // reinicia estadocorreción
  mostrarResultados.value = false; // oculta resultados

  cargarPreguntas();
}

// Lo lanza btn INICIAR de configuración de preguntas 
async function lanzarPreguntas() {
  try {
    await cargarPreguntas();

  } catch (error) {
    console.error('Error al cargar preguntas:', error);
    Swal.fire({
      icon: 'error',
      title: 'Error',
      text: error.message || 'Error al cargar las preguntas',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
    });
  }
}


// Desbloquea los selects y poder elegir otra configuración con btn RESETEAR
function resetearSettings() {
  // Resetear todos los valores
  filtro.value = {
    cantidad: 5,
    bloqueSeleccionado: '',
    temaSeleccionado: ''
  };
  modoSeleccion.value = ''; // Resetear modo de selección
  preguntas.value = []; // Limpiar preguntas cargadas
  respuestasUsuario.value = {}; // Limpiar respuestas
  examenCorregido.value = false; // Resetear estado de corrección
  mostrarResultados.value = false; // Ocultar resultados
}

// CORRECIÖN EXAMEN //////////////////////////////////////////////////////////////////////
async function corregirExamen() {
  // Verificar si hay usuario logueado
  const usuarioActual = servicioUsuario.obtenerSesion();

  // Si no hay usuario logueado advertencia
  if (!usuarioActual) {
    await Swal.fire({
      icon: 'warning',
      title: 'No hay sesión activa',
      text: 'Debes iniciar sesión para guardar los resultados',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
    });
    return;
  }

  try {
    // Para contar aciertos y guardar resultados
    let aciertos = 0;
    let erroresGuardado = 0;

    // Recorrer cada pregunta para verificar si la respuesta del usuario es correcta
    for (const pregunta of preguntas.value) {
      const esAcierto = pregunta.Respuesta === respuestasUsuario.value[pregunta.Id];
      if (esAcierto) aciertos++;

      // Guardar el resultado de cada pregunta
      try {
        await servicioResultado.guardar({
          UsuarioId: usuarioActual.Id,
          PreguntaId: pregunta.Id,
          Acierto: esAcierto
        });

      } catch (error) {

        console.error('Error al guardar resultado:', error);
        erroresGuardado++;
      }
    }

    // Calculo nota final
    let totalPreguntas = preguntas.value.length;
    let nota = (aciertos / totalPreguntas) * 10;

    // Actualizar el estado con los resultados
    resultados.value = {
      aciertos: aciertos,
      total: totalPreguntas,
      nota: nota.toFixed(2) // 2 decimales
    };

    // Marcar examen como corregido y mostrar resultados
    examenCorregido.value = true;
    mostrarResultados.value = true;

    // Si errores guardardo advertencia
    if (erroresGuardado > 0) {
      await Swal.fire({
        icon: 'warning',
        title: 'Advertencia',
        text: `Se guardaron los resultados pero hubo ${erroresGuardado} errores.`,
        confirmButtonText: 'Entendido',
        background: '#e5e5e5',
        color: '#770494d2',
      });
    }

  } catch (error) {
    console.error('Error en el proceso de corrección:', error);
    Swal.fire({
      icon: 'error',
      title: 'Error',
      text: 'Hubo un error al procesar los resultados del examen',
      confirmButtonText: 'Aceptar',
      background: '#e5e5e5',
      color: '#770494d2',
    });
  }
}

// Ocultar el modal de ventana emergente
//function cerrarResultados() {
// mostrarResultados.value = false;
//}
///////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// TEMPORIZADOR EXAMEN
const tiempoPregunta = 1.10; //  120 minutos / 110 preguntas redondeado

// Calcular tiempo total por numero de preguntas
const calcularTiempoExamen = computed(() => {
  return Math.ceil(filtro.value.cantidad * tiempoPregunta);
});

// Cuando se acabe tiempo
const tiempoTerminado = () => {
  // Corregir automáticamente o mostrar mensaje
  corregirExamen();
};

// Quitar temporizador al salir!!!!!!!!!!!

//////////////////////////////////////////////////////////////////////////
</script>

<template>
  <section class="examen">
    <!-- Configuración -->
    <div class="examen-config">
      <h2 class="examen-titulo">Cuestionario TAI</h2>

      <div class="examen-contenedor">
        <!-- Grupo de selects -->
        <div class="filtros-container">
          <div class="filtro-item">
            <label>Preguntas</label>
            <select v-model="filtro.cantidad" class="examen-selector">
              <option :value="5">5</option>
              <option :value="10">10</option>
              <option :value="20">20</option>
            </select>
          </div>

          <div class="filtro-item">
            <label>Bloque</label>
            <select v-model="filtro.bloqueSeleccionado" class="examen-selector" :disabled="modoSeleccion === 'tema'"
              @change="seleccionarModoBloque"><!-- disabled deshabilita el select si modoSeleccion es 'tema' -->
              <option value="">Selecciona un bloque</option>
              <option v-for="bloque in bloques" :key="bloque.Bloque" :value="bloque.Bloque">
                {{ bloque.Bloque }} ({{ bloque.TotalPreguntas }} preguntas)
              </option>
            </select>
          </div>

          <div class="filtro-item">
            <label>Tema</label>
            <select v-model="filtro.temaSeleccionado" class="examen-selector" :disabled="modoSeleccion === 'bloque'"
              @change="seleccionarModoTema">
              <option value="">Selecciona un tema</option> <!--Simplificar el endpoint para que funciones -->
              <option v-for="tema in temas" :key="tema.Tema" :value="tema.Tema">
                {{ tema.Tema }}
              </option>
            </select>
          </div>
        </div>

        <!-- Botones configuración -->
        <div class="botones-container">

          <button @click.prevent="resetearSettings" class="boton btn-reset"
            :disabled="!filtro.cantidad || (!filtro.bloqueSeleccionado && !filtro.temaSeleccionado)">
            Resetear
          </button>

          <button @click="reiniciarExamen" class="boton btn-nuevoExamen"
            :disabled="!filtro.cantidad || (!filtro.bloqueSeleccionado && !filtro.temaSeleccionado)">
            Nuevo Examen
          </button>

          <button @click.prevent="lanzarPreguntas" class="boton btn-iniciar"
            :disabled="!filtro.cantidad || (!filtro.bloqueSeleccionado && !filtro.temaSeleccionado)">
            Iniciar
          </button>
        </div>


      </div>
    </div>

    <!-- Titular dinamico del tema o bloque elegido -->
    <h3 class="tituloPreguntas" v-if="preguntas.length > 0">{{ filtro.temaSeleccionado }} {{ filtro.bloqueSeleccionado
      }}</h3>


    <!-- Preguntas -->
    <div class="preguntas">
      <div v-for="pregunta in preguntas" :key="pregunta.Id" class="pregunta">
        <div class="pregunta-info">
          <span class="pregunta-id">ID: {{ pregunta.Id }}</span>
          <div class="pregunta-metadata">
            <span class="pregunta-bloque">{{ pregunta.Bloque }}</span>
            <span class="pregunta-separator">|</span>
            <span class="pregunta-origen">{{ pregunta.Origen }}</span>
          </div>
        </div>

        <p class="pregunta-texto">{{ pregunta.Pregunta }}</p>

        <div class="pregunta-opciones">
          <label v-for="opcion in ['Opcion1', 'Opcion2', 'Opcion3', 'Opcion4']" :key="opcion" class="pregunta-opcion">
            <!-- Crea un botón de radio para cada opción, el name es para que los radio estén agrupados por pregunta y solo se pueda elegir una -->
            <input type="radio" :name="'pregunta-' + pregunta.Id" :value="pregunta[opcion]"
              v-model="respuestasUsuario[pregunta.Id]" :disabled="examenCorregido" class="pregunta-radio">

            <!-- el 2º class aplica estilos dependiendo de si la opción es correcta o incorrecta -->
            <span class="pregunta-opcion-texto" :class="{
              'pregunta-opcion-textoCorrecta': examenCorregido && pregunta[opcion] === pregunta.Respuesta,
              'pregunta-opcion-textoIncorrecta': examenCorregido && respuestasUsuario[pregunta.Id] === pregunta[opcion] && pregunta[opcion] !== pregunta.Respuesta
            }">
              {{ pregunta[opcion] }}
            </span>
          </label>
        </div>
      </div>
    </div>

    <!-- Botones control examen -->
    <div class="examen-controles" v-if="preguntas.length > 0">
      <button @click.prevent="resetearSettings" class="boton btn-reset"
        :disabled="!filtro.cantidad || (!filtro.bloqueSeleccionado && !filtro.temaSeleccionado)">
        Salir
      </button>
      <button @click="corregirExamen" class="boton btn-corregir" :disabled="examenCorregido">
        Corregir
      </button>

    </div>

    <!-- Ventana emergente resultados -->
    <ResultadosModal 
      v-if="mostrarResultados" 
      :resultados="resultados" 
      @close="mostrarResultados = false" 
    />

  </section>

  <!-- Mostrar temporizador cuando hay preguntas y no esta corregido-->
  <TemporizadorEx 
    v-if="preguntas.length > 0 && !examenCorregido"
    :tiempoMinutos="calcularTiempoExamen"
    @tiempoTerminado="tiempoTerminado" 
  />



</template>

<style scoped>
.examen {
  max-width: 75%;
  margin: 0 auto;
  margin-top: 50px;
  margin-bottom: 80px;
  padding-top: 80px;
  padding-bottom: 80px;
  padding-left: 8%;
  padding-right: 8%;
  color: var(--color-texto-principal);
  background-color: var(--color-fondo2);
  border-radius: 5px;
  
}

/* Configuración y Contenedor */
.examen-config {
  background-color: var(--color-fondo);
  padding: 20px;
  margin-bottom: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.examen-titulo {
  font-size: 2rem;
  margin-bottom: 20px;
  text-align: center;
  padding-top: 10px;
  padding-bottom: 15px;
  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 10px;
  text-decoration-color: var(--color-resaltar);
}

.examen-contenedor {
  display: flex;
  flex-direction: column;
  gap: 20px;
  align-items: center;
}

/* Filtros y Selects */
.filtros-container {
  display: flex;
  gap: 1rem;
  margin-bottom: 1rem;
  flex-wrap: wrap;
  justify-content: center;
  width: 100%;
}

.filtro-item {
  display: flex;
  flex-direction: column;
  gap: 8px;
  min-width: 200px;
  flex: 1;
}

.filtro-item label {
  font-weight: 500;
  color: #333;
}

.examen-selector {
  width: 100%;
  padding: 8px;
  border: 1px solid #dddddd;
  border-radius: 4px;
  background-color: white;
}

.examen-selector:disabled {
  background-color: #f5f5f5;
  cursor: not-allowed;
  opacity: 0.7;
}

/* Botones Container */
.botones-container {
  display: flex;
  gap: 1rem;
  justify-content: center;
  padding-top: 20px;
  padding-bottom: 50px;
  width: 100%;
  max-width: 400px;
}

/* Botones Estilos */
.boton {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1.1rem;
  transition: background-color 0.2s;
  min-width: 180px;
  min-height: 50px;
}

.btn-nuevoExamen {
  background-color: var(--color-primario);
  color: white;
}

.btn-nuevoExamen:hover {
  background-color: #006abb;
}

.btn-nuevoExamen:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.btn-iniciar {
  background-color: var(--color-secundario);
  color: white;
}

.btn-iniciar:hover {
  background-color: #01d07d;
}

.btn-iniciar:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.btn-reset {
  background-color: #6c757d;
  color: white;
}

.btn-reset:hover {
  background-color: #5a6268;
}

.btn-reset:disabled {
  background-color: #cccccc;
  cursor: not-allowed;
}

.tituloPreguntas {

  color: #333333;
  padding-top: 20px;
  padding-bottom: 20px;
}

/* Preguntas */
.preguntas {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.pregunta {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.pregunta-info {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
  padding-bottom: 10px;
  border-bottom: 1px solid #eee;
}

.pregunta-metadata {
  display: flex;
  align-items: center;
  gap: 8px;
}

.pregunta-texto {
  font-weight: bold;
  margin-bottom: 15px;
}

/* Opciones de Pregunta */
.pregunta-opciones {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.pregunta-opcion {
  display: flex;
  align-items: center;
  padding: 8px;
  cursor: pointer;
  border-radius: 4px;
  transition: background-color 0.2s;
}

.pregunta-opcion:hover {
  background-color: #f5f5f5;
}

.pregunta-radio {
  margin-right: 10px;
}

.pregunta-opcion-texto {
  font-size: 1em;
  width: 100%;
}

/* Estilos para respuestas correctas/incorrectas */
.pregunta-opcion-textoCorrecta {
  color: #2e7d32;
  font-weight: bold;
  background-color: #e8f5e9;
  padding: 4px 8px;
  border-radius: 4px;
  width: 100%;
}

.pregunta-opcion-textoIncorrecta {
  color: #c62828;
  font-weight: bold;
  background-color: #ffebee;
  padding: 4px 8px;
  border-radius: 4px;
  width: 100%;
}

.pregunta-opcion-textoCorrecta::after {
  content: "✓";
  font-size: 14px;
  margin-left: 8px;
}

.pregunta-opcion-textoIncorrecta::after {
  content: "❌";
  font-size: 14px;
  margin-left: 8px;
}

/* Identificadores y Metadatos */
.pregunta-id {
  color: #666;
  font-size: 0.9em;
  padding: 2px 8px;
  background-color: var(--color-fondo2);
  border-radius: 4px;
}

.pregunta-bloque {
  color: #36148c;
  font-weight: 600;
  font-size: 0.9em;
  padding: 2px 8px;
  background-color: #e6e5f5;
  border-radius: 4px;
}

.pregunta-origen {
  color: #0033a2;
  font-weight: 600;
  font-size: 0.9em;
  padding: 2px 8px;
  background-color: #e3f2fd;
  border-radius: 4px;
}

.pregunta-separator {
  color: #ccc;
}

/* Controles del Examen */
.examen-controles {
  font-size: 1.1rem;
  margin-top: 40px;
  display: flex;
  justify-content: center;
  gap: 18px;
}

.btn-corregir {

  background-color: #8c1414;
  color: white;
}

.btn-corregir:hover {
  background-color: #d41f1f;
}





/* Responsive */
@media (max-width: 768px) {
  .examen {
    margin-top: 25%;
    padding: 10px;
  }

  .filtros-container {
    flex-direction: column;
    align-items: stretch;
  }

  .filtro-item {
    width: 100%;
  }

  .examen-selector {
    width: 100%;
  }

  .botones-container {
    flex-direction: column;
  }

  .boton {
    width: 100%;
  }

  .pregunta {
    padding: 15px;
  }

  .modal-contenido {
    width: 90%;
    padding: 20px;
  }

  .resultados-numero--grande {
    font-size: 36px;
  }
}


</style>
