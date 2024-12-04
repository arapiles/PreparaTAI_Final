<script setup>
import { ref, onMounted } from 'vue';
import servicioResultado from '../services/servicioResultado';
import servicioUsuario from '../services/servicioUsuario';
import Swal from 'sweetalert2';

const tituloH1 = ref("Mis Estadísticas");
const descripcion = ref("Revisa tu progreso en cada bloque del temario");

const estadisticas = ref(null); // Estadísticas globales
const estadisticasTemas = ref(null);
const cargando = ref(true); // Bandera para controlar y mostrar el estado de carga de las estadísticas. Se usa en el template para controlar la visibilidad de los stats

// Función para cargar las estadísticas de bloques
async function cargarEstadisticas() {
  try {
    // Obtener usuario actual
    const usuario = servicioUsuario.obtenerSesion();

    if (!usuario) {
      await Swal.fire({
        icon: 'warning',
        title: 'No hay sesión activa',
        text: 'Debes iniciar sesión para ver tus estadísticas',
        confirmButtonText: 'Aceptar',
        background: '#e5e5e5',
        color: '#770494d2',
        confirmButtonColor: '#780494'
      });
      return;
    }

    // Llama al servicio para obtener las estadísticas de bloques
    const response = await servicioResultado.getEstadisticasBloques(usuario.Id);

    // Ordenar los bloques
    /* Error cuando el usuario no tiene estadísticas
    if (response.data.estadisticas && response.data.estadisticas.ResultadosPorBloque) {
      response.data.estadisticas.ResultadosPorBloque.sort((a, b) => 
        a.Bloque.localeCompare(b.Bloque)
      );
    }
    */

    if (!response.data.estadisticas || !response.data.estadisticas.ResultadosPorBloque) {
      // Carga estadísticas en blanco
      estadisticas.value = {
        TotalPreguntas: 0,
        Aciertos: 0,
        PorcentajeAciertos: 0,
        ResultadosPorBloque: []
      };
      return;
    }

    response.data.estadisticas.ResultadosPorBloque.sort((a, b) =>
      a.Bloque.localeCompare(b.Bloque)
    );

    // Actualizar variable con stats
    estadisticas.value = response.data.estadisticas;


  } catch (error) {
    console.error('Error:', error);
    estadisticas.value = {
      TotalPreguntas: 0,
      Aciertos: 0,
      PorcentajeAciertos: 0,
      ResultadosPorBloque: []
    };

  } finally {
    cargando.value = false;
  }
}

async function cargarEstadisticasTemas() {
  try {
    const usuario = servicioUsuario.obtenerSesion();
    if (!usuario) {
      await Swal.fire({
        icon: 'warning',
        title: 'No hay sesión activa',
        text: 'Debes iniciar sesión para ver tus estadísticas',
        confirmButtonText: 'Aceptar',
        background: '#e5e5e5',
        color: '#770494d2',
        confirmButtonColor: '#780494'
      });
      return;
    }

    const response = await servicioResultado.getEstadisticasTemas(usuario.Id);

    /** Error usuario nuevo carga estadísticas  
    if (response.data.estadisticas && response.data.estadisticas.ResultadosPorTema) {
      response.data.estadisticas.ResultadosPorTema.sort((a, b) => 
        a.Tema.localeCompare(b.Tema)
      );
    }*/

    if (!response.data.estadisticas || !response.data.estadisticas.ResultadosPorTema) {
      estadisticasTemas.value = {
        TotalPreguntas: 0,
        Aciertos: 0,
        PorcentajeAciertos: 0,
        ResultadosPorTema: []
      };
      return;
    }

    response.data.estadisticas.ResultadosPorTema.sort((a, b) =>
      a.Tema.localeCompare(b.Tema)
    );


    estadisticasTemas.value = response.data.estadisticas;


  } catch (error) {
    console.error('Error:', error);
    estadisticasTemas.value = {
      TotalPreguntas: 0,
      Aciertos: 0,
      PorcentajeAciertos: 0,
      ResultadosPorTema: []
    };

  } finally {
    cargando.value = false;
  }
}

// Cargar stats cuando se monta el componente
onMounted(() => {
  cargarEstadisticas();
  cargarEstadisticasTemas();

});
</script>

<template>
  <main class="estadisticas">
    <!-- cabecera Section -->
    <section class="cabecera">
      <div class="cabecera-contenido">
        <h1 class="cabecera-titulo">{{ tituloH1 }}</h1>
        <p class="cabecera-descripcion">{{ descripcion }}</p>
      </div>
    </section>

    <!-- Estadísticas Globales -->
    <section class="stats" v-if="!cargando && estadisticas">
      <div class="stats-contenedor">
        <div class="stat-card">
          <div class="stat-card-icono">📝</div>
          <h3 class="stat-card-titulo">Total Respondidas</h3>
          <p class="stat-card-numero">{{ estadisticas.TotalPreguntas }}</p>
        </div>

        <div class="stat-card">
          <div class="stat-card-icono">✅</div>
          <h3 class="stat-card-titulo">Aciertos</h3>
          <p class="stat-card-numero">{{ estadisticas.Aciertos }}</p>
        </div>

        <div class="stat-card">
          <div class="stat-card-icono">📊</div>
          <h3 class="stat-card-titulo">Porcentaje Éxito</h3>
          <p class="stat-card-numero">{{ estadisticas.PorcentajeAciertos }}%</p>
        </div>
      </div>
    </section>

    <!-- Estadísticas por Bloque -->
    <section class="bloques" v-if="!cargando && estadisticas">
      <div class="bloques-contenedor">
        <h2 class="seccion-titulo">Resultados por Bloque</h2>

        <div class="bloques-contenedor-cards">
          <div v-for="bloque in estadisticas.ResultadosPorBloque" :key="bloque.Bloque" class="bloque-card">
            <h3 class="bloque-card-titulo">{{ bloque.Bloque }}</h3>
            <div class="bloque-card-stats">
              <div class="bloque-card-dato">
                <span>Total: {{ bloque.Total }}</span>
              </div>
              <div class="bloque-card-dato">
                <span>Aciertos: {{ bloque.Aciertos }}</span>
              </div>
            </div>
            <div class="bloque-card-progreso">
              <div class="progreso">
                <div class="progreso-barra" :style="{ width: bloque.PorcentajeAciertos + '%' }" :class="{
                  'success': bloque.PorcentajeAciertos >= 70,
                  'warning': bloque.PorcentajeAciertos >= 40 && bloque.PorcentajeAciertos < 70,
                  'danger': bloque.PorcentajeAciertos < 40
                }">
                </div>
                <span class="progreso-texto">{{ bloque.PorcentajeAciertos }}%</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- Estadísticas por Tema -->
    <section class="temas" v-if="!cargando && estadisticasTemas">
      <div class="temas-contenedor">
        <h2 class="seccion-titulo">Resultados por Tema</h2>

        <div class="temas-columnas">
          <!-- Columna B1 -->
          <div class="temas-columna">
            <h3 class="temas-bloque-titulo">B1</h3>
            <div class="temas-lista">
              <div v-for="tema in estadisticasTemas.ResultadosPorTema.filter(t => t.Tema.startsWith('B1'))"
                :key="tema.Tema" class="tema-card">
                <h4 class="tema-card-titulo">{{ tema.Tema }}</h4>
                <div class="tema-card-stats">
                  <div class="tema-card-dato">
                    <span>Total: {{ tema.Total }}</span>
                  </div>
                  <div class="tema-card-dato">
                    <span>Aciertos: {{ tema.Aciertos }}</span>
                  </div>
                </div>
                <div class="tema-card-progreso">
                  <div class="progreso">
                    <div class="progreso-barra" :style="{ width: tema.PorcentajeAciertos + '%' }" :class="{
                      'success': tema.PorcentajeAciertos >= 70,
                      'warning': tema.PorcentajeAciertos >= 40 && tema.PorcentajeAciertos < 70,
                      'danger': tema.PorcentajeAciertos < 40
                    }">
                    </div>
                    <span class="progreso-texto">{{ tema.PorcentajeAciertos }}%</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Columna B2 -->
          <div class="temas-columna">
            <h3 class="temas-bloque-titulo">B2</h3>
            <div class="temas-lista">
              <div v-for="tema in estadisticasTemas.ResultadosPorTema.filter(t => t.Tema.startsWith('B2'))"
                :key="tema.Tema" class="tema-card">
                <h4 class="tema-card-titulo">{{ tema.Tema }}</h4>
                <div class="tema-card-stats">
                  <div class="tema-card-dato">
                    <span>Total: {{ tema.Total }}</span>
                  </div>
                  <div class="tema-card-dato">
                    <span>Aciertos: {{ tema.Aciertos }}</span>
                  </div>
                </div>
                <div class="tema-card-progreso">
                  <div class="progreso">
                    <div class="progreso-barra" :style="{ width: tema.PorcentajeAciertos + '%' }" :class="{
                      'success': tema.PorcentajeAciertos >= 70,
                      'warning': tema.PorcentajeAciertos >= 40 && tema.PorcentajeAciertos < 70,
                      'danger': tema.PorcentajeAciertos < 40
                    }">
                    </div>
                    <span class="progreso-texto">{{ tema.PorcentajeAciertos }}%</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Columna B3 -->
          <div class="temas-columna">
            <h3 class="temas-bloque-titulo">B3</h3>
            <div class="temas-lista">
              <div v-for="tema in estadisticasTemas.ResultadosPorTema.filter(t => t.Tema.startsWith('B3'))"
                :key="tema.Tema" class="tema-card">
                <h4 class="tema-card-titulo">{{ tema.Tema }}</h4>
                <div class="tema-card-stats">
                  <div class="tema-card-dato">
                    <span>Total: {{ tema.Total }}</span>
                  </div>
                  <div class="tema-card-dato">
                    <span>Aciertos: {{ tema.Aciertos }}</span>
                  </div>
                </div>
                <div class="tema-card-progreso">
                  <div class="progreso">
                    <div class="progreso-barra" :style="{ width: tema.PorcentajeAciertos + '%' }" :class="{
                      'success': tema.PorcentajeAciertos >= 70,
                      'warning': tema.PorcentajeAciertos >= 40 && tema.PorcentajeAciertos < 70,
                      'danger': tema.PorcentajeAciertos < 40
                    }">
                    </div>
                    <span class="progreso-texto">{{ tema.PorcentajeAciertos }}%</span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Columna B4 -->
          <div class="temas-columna">
            <h3 class="temas-bloque-titulo">B4</h3>
            <div class="temas-lista">
              <div v-for="tema in estadisticasTemas.ResultadosPorTema.filter(t => t.Tema.startsWith('B4'))"
                :key="tema.Tema" class="tema-card">
                <h4 class="tema-card-titulo">{{ tema.Tema }}</h4>
                <div class="tema-card-stats">
                  <div class="tema-card-dato">
                    <span>Total: {{ tema.Total }}</span>
                  </div>
                  <div class="tema-card-dato">
                    <span>Aciertos: {{ tema.Aciertos }}</span>
                  </div>
                </div>
                <div class="tema-card-progreso">
                  <div class="progreso">
                    <div class="progreso-barra" :style="{ width: tema.PorcentajeAciertos + '%' }" :class="{
                      'success': tema.PorcentajeAciertos >= 70,
                      'warning': tema.PorcentajeAciertos >= 40 && tema.PorcentajeAciertos < 70,
                      'danger': tema.PorcentajeAciertos < 40
                    }">
                    </div>
                    <span class="progreso-texto">{{ tema.PorcentajeAciertos }}%</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  </main>


</template>

<style scoped>
.estadisticas {
  background-color: #ffffff;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.cabecera {
  background: linear-gradient(45deg, var(--color-terciario), var(--color-resaltar2));
  padding: 4rem 2rem;
  display: flex;
  align-items: center;
  justify-content: center;
  color: var(--color-primario);
}

.cabecera-contenido {
  text-align: center;
}

.cabecera-titulo {
  font-size: 3rem;
  font-weight: 800;
  margin-bottom: 1.5rem;
}

.cabecera-descripcion {
  font-size: 1.25rem;
  opacity: 0.9;
}

.stats {
  padding: 4rem 2rem;
  background-color: #f8f9fa;
}

.stats-contenedor {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: space-around;
  gap: 2rem;
  flex-wrap: wrap;
}

.stat-card {
  flex: 1;
  min-width: 250px;
  max-width: 350px;
  text-align: center;
  padding: 2rem;
  background: var(--color-fondo);
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;
}

.stat-card:hover {
  transform: translateY(-5px);
}

.stat-card-icono {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.stat-card-titulo {
  font-size: 1.25rem;
  font-weight: bold;
  color: var(--color-texto-secundario);
  margin-bottom: 0.5rem;
}

.stat-card-numero {
  font-size: 2rem;
  font-weight: bold;
  color: #4373bc;
}

.bloques {
  padding: 4rem 2rem;
  background-color: white;
  flex: 1;
}

.bloques-contenedor {
  max-width: 1200px;
  margin: 0 auto;
}

.seccion-titulo {
  font-size: 2rem;
  padding-bottom: 10px;
  color: var(--color-primario);
  margin-bottom: 2rem;
  text-align: center;
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);
}

.bloques-contenedor-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
}

.bloque-card {
  flex: 1;
  min-width: 200px;
  max-width: 400px;
  background: #f8f9fa;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.bloque-card-titulo {
  font-size: 1.5rem;
  color: var(--color-primario);
  margin-bottom: 1rem;
  font-weight: bold;

}

.bloque-card-stats {
  display: flex;
  justify-content: space-between;
  margin-bottom: 1rem;

}

.bloque-card-dato {
  color: var(--color-texto-secundario);
}

.progreso {
  background: #e9ecef;
  border-radius: 20px;
  height: 20px;
  position: relative;
  overflow: hidden;
}

.progreso-barra {
  height: 100%;
  border-radius: 20px;
  transition: width 0.3s ease;
}

.progreso-barra.success {
  background: #28a745;
}

.progreso-barra.warning {
  background: #ffc107;
}

.progreso-barra.danger {
  background: #dc3545;
}

.progreso-texto {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  color: #333;
  font-size: 0.9rem;
  font-weight: bold;
}

.temas {
  padding: 4rem 2rem;
  background-color: #f8f9fa;
  flex: 1;
}

.temas-contenedor {
  max-width: 1200px;
  margin: 0 auto;
}

.temas-contenedor-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
}

.tema-card {
  flex: 1;
  min-width: 200px;
  max-width: 250px;
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.tema-card-titulo {
  font-size: 1rem;
  color: #333;
  margin-bottom: 1rem;
  font-weight: bold;

  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.tema-card-stats {
  display: flex;
  justify-content: space-between;
  margin-bottom: 1rem;
}

.tema-card-dato {
  color: #666;
}


.temas-columnas {
  display: flex;
  gap: 2rem;
}

.temas-columna {
  flex: 1;
}

.temas-bloque-titulo {
  text-align: center;
  font-size: 1.5rem;
  margin-bottom: 1rem;
  padding: 0.5rem;
  background: #f8f9fa;
  border-radius: 8px;
  color: var(--color-primario);

}

.temas-lista {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

@media (max-width: 768px) {
  .cabecera-titulo {
    padding-top: 3.5rem;
    font-size: 2rem;
  }

  .cabecera-descripcion {
    font-size: 1rem;
  }

  .stats,
  .bloques {
    padding: 2rem 1rem;
  }

  .stat-card {
    min-width: 100%;
  }

  .bloque-card {
    min-width: 100%;
  }

  .temas-columnas {
    flex-direction: column;
    gap: 1rem;
  }

  .temas-columna {
    width: 100%;
  }

  .tema-card {
    max-width: 100%;
  }
}
</style>