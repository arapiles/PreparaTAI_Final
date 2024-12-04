<script setup>
import { ref, onMounted } from 'vue';
import servicioPregunta from "../../services/servicioPregunta.js";
import servicioUsuario from "../../services/servicioUsuario.js";
import Swal from "sweetalert2";

// Almacena stats preguntas
const estadisticasPreguntas = ref({
  total: 0,
  porBloque: [],
  porTema: []
});
// Alamcena stats usuarios
const estadisticasUsuarios = ref({
  total: 0,
  administradores: 0,
  normales: 0
});

// Cargar datos de preguntas
async function cargarEstadisticasPreguntas() {
  try {
    // Obtener total preguntas
    const responsePreguntas = await servicioPregunta.getAll();
    // Se guarda la cantidad total de preguntas recibidas del servidor
    estadisticasPreguntas.value.total = responsePreguntas.data.response.length;

    // Obtener preguntas por bloque
    const responseBloques = await servicioPregunta.getListaBloques();
    // Se guarda la cantidad de preguntas por bloque recibidas 
    estadisticasPreguntas.value.porBloque = responseBloques.data.bloques;

    // Obtener preguntas por tema
    const responseTemas = await servicioPregunta.getListaTemas();
    estadisticasPreguntas.value.porTema = responseTemas.data.temas;

  } catch (error) {
    console.error('Error al cargar estadísticas de preguntas:', error);
    mensajeError("Error al cargar estadísticas de preguntas");
  }
}

// Cargar datos de usuarios
async function cargarEstadisticasUsuarios() {
  try {
    const response = await servicioUsuario.getAll(); // Obtener lista usuarios y
    const usuarios = response.data.response; // guardar respuesta del servidor

    // Calcula stats usuarios
    estadisticasUsuarios.value = {
      total: usuarios.length,
      administradores: usuarios.filter(u => u.TipoUsuario === 1).length, // Total admin
      normales: usuarios.filter(u => u.TipoUsuario === 2).length // Total usuarios
    };

  } catch (error) {

    console.error('Error al cargar estadísticas de usuarios:', error);
    mensajeError("Error al cargar estadísticas de usuarios");
  }
}

onMounted(async () => {
  await Promise.all([ // Ejecutar ambas funciones en paralelo
    cargarEstadisticasPreguntas(),
    cargarEstadisticasUsuarios()
  ]);
});

function mensajeError(mensaje) {
  Swal.fire({
    title: "Error",
    text: mensaje,
    icon: "error",
    background: '#e5e5e5',
    color: '#770494d2',
  });
}
</script>


<template>
  <div class="dashboard">
    <h1 class="dashboard-titulo">Panel de Administración</h1>

    <!-- Estadísticas Generales -->
    <section class="estadisticas">
      <h2 class="estadisticas-titulo">Estadísticas Generales</h2>

      <div class="estadisticas-contenedor">

        <!-- Card Total Preguntas -->
        <div class="card-stats">
          <div class="card-stats-header">
            <h3 class="card-stats-titulo">Total Preguntas</h3>
          </div>
          <div class="card-stats-contenido">
            <span class="card-stats-numero">{{ estadisticasPreguntas.total }}</span>
            <p class="card-stats-descripcion">Preguntas en el sistema</p>
          </div>

        </div>

        <!-- Card Total Usuarios -->
        <div class="card-stats">
          <div class="card-stats-header">
            <h3 class="card-stats-titulo">Total Usuarios</h3>
          </div>
          <div class="card-stats-contenido">
            <span class="card-stats-numero">{{ estadisticasUsuarios.total }}</span>
            <p class="card-stats-descripcion">Usuarios registrados</p>
          </div>
        </div>

        <!-- Card Tipos Usuario -->
        <div class="card-stats">
          <div class="card-stats-header">
            <h3 class="card-stats-titulo">Tipos de Usuario</h3>
          </div>
          <div class="card-stats-contenido">
            <div class="card-stats-estadistica">
              <span class="card-stats-label">Administradores:</span>
              <span class="card-stats-valor">{{ estadisticasUsuarios.administradores }}</span>
            </div>
            <div class="card-stats-estadistica">
              <span class="card-stats-label">Usuarios:</span>
              <span class="card-stats-valor">{{ estadisticasUsuarios.normales }}</span>
            </div>
          </div>
        </div>

      </div>
    </section>

    <!-- Preguntas por Bloque -->
    <section class="bloques">
      <h2 class="bloques-titulo">Preguntas por Bloque</h2>

      <div class="bloques-contenedor">
        <div v-for="bloque in estadisticasPreguntas.porBloque" :key="bloque.Bloque" class="card-bloque">
          <div class="card-bloque-header">
            <h3 class="card-bloque-titulo">{{ bloque.Bloque }}</h3>
          </div>
          <div class="card-bloque-contenido">
            <span class="card-bloque-numero">{{ bloque.TotalPreguntas }}</span>
            <p class="card-bloque-descripcion">Preguntas</p>
            <span class="card-bloque-subtexto">{{
              bloque.Bloque === 'B1' ? '9 Temas' :
                bloque.Bloque === 'B2' ? '5 Temas' :
                  bloque.Bloque === 'B3' ? '9 Temas' :
                    bloque.Bloque === 'B4' ? '10 Temas' : ''
              }}</span>
          </div>
        </div>
      </div>
    </section>

    <!-- Preguntas por Tema -->
    <section class="temas">
      <h2 class="temas-titulo">Distribución por Temas</h2>

      <div class="temas-contenedor">
        <table class="tabla-temas">
          <thead>
            <tr>
              <th>Tema</th>
              <th class="tabla-temas-columna-numero">Preguntas</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="tema in estadisticasPreguntas.porTema" :key="tema.Tema">
              <td class="tabla-temas-tema">{{ tema.Tema }}</td>
              <td class="tabla-temas-columna-numero">{{ tema.TotalPreguntas }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </section>


  </div>
</template>

<style scoped>
.dashboard {
  max-width: 80%;
  margin: 0 auto;
  margin-top: 30px;
  margin-bottom: 30px;
  padding: 2rem;
  background-color: #eee;
  border-radius: 8px;
}

.dashboard-titulo {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 2rem;
  color: #333;

  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);
}

/* Estadísticas Generales */
.estadisticas-contenedor {
  display: flex;
  justify-content: space-between;
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.card-stats {
  flex: 1;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 1.5rem;
  transition: transform 0.2s ease;
  color: #000;
}


.card-stats-titulo {
  font-size: 1.25rem;
  color: var(--color-primario);
  margin-bottom: 1rem;
}

.card-stats-contenido {
  text-align: center;
}

.card-stats-numero {
  font-size: 2.5rem;
  font-weight: bold;
  color: rgb(50, 128, 176);
  display: block;
  margin-bottom: 0.5rem;
}

.card-stats-descripcion {
  color: #666;
  margin-bottom: 0.5rem;
}

.card-stats-estadistica {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.5rem;
  padding: 0.5rem 0;
  border-bottom: 1px solid #eee;
}

/* Bloques */
.bloques-contenedor {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 2rem;
}

.card-bloque {
  flex: 1;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 1rem;
  text-align: center;
  transition: transform 0.2s ease;
}



.card-bloque-titulo {
  font-weight: bold;
  font-size: 1.1rem;
  color: var(--color-texto-principal);
  margin-bottom: 0.5rem;
}

.card-bloque-numero {
  font-size: 2rem;
  font-weight: bold;
  color: var(--color-texto-secundario);
  display: block;
}

.card-bloque-descripcion {
  color: #666;
  margin: 0.25rem 0;
}

.card-bloque-subtexto {
  font-size: 0.9rem;
  color: #999;
}

/* Tabla de Temas */
.tabla-temas {
  width: 100%;
  border-collapse: collapse;
  background: white;
  color: #232323;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.tabla-temas th,
.tabla-temas td {
  padding: 1rem;

  border-bottom: 1px solid #eee;
}

.tabla-temas th {
  background-color: var(--color-terciario);
  color: var(--color-primario);
  font-weight: bold;
  text-align: left;
}

.tabla-temas tr:hover {
  background-color: #f5f5f5;
}

.tabla-temas-tema {
  max-width: 400px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.tabla-temas-columna-numero {
  width: 120px;
  color: var(--color-primario);
  font-weight: bold;
  text-align: center;
}



/* Títulos de secciones */
.estadisticas-titulo,
.bloques-titulo,
.temas-titulo {
  font-size: 1.5rem;
  color: var(--color-primario);
  margin-bottom: 1.5rem;
  font-weight: bold;
}



@media (max-width: 768px) {
  .dashboard {
    margin-top: 8rem;
    padding: 1rem;
  }

  .estadisticas-contenedor {
    flex-direction: column;
  }

  .bloques-contenedor {
    flex-direction: column;
  }

  .card-bloque {
    width: 100%;
  }

  .tabla-temas {
    width: 100%;
    display: block;
    overflow-x: auto;
  }

  .tabla-temas-tema {
    max-width: 300px;
    /* Ajusta los temas para que se vea el nº de preguntas en mv */
  }

  .dashboard-nav {
    flex-direction: column;
  }

  .dashboard-link {
    width: 100%;
    text-align: center;
  }
}
</style>