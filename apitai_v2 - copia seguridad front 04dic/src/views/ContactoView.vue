<script setup>
import { ref } from "vue";

const tituloH1 = ref("Contacta con PreparaTAI");
const descripcion = ref("Estamos aquí para ayudarte en tu camino hacia el éxito. Contáctanos para cualquier duda o sugerencia.");

// Información de contacto para recorrer con el for en el template
const contactInfo = ref([
  {
    titulo: "Email",
    descripcion: "soporte@preparatai.es",
    icono: `<svg height="64px" width="64px" version="1.1" id="_x32_" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 512 512" xml:space="preserve" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <style type="text/css"> .st0{fill:#000000;} </style> <g> <path class="st0" d="M510.746,110.361c-2.128-10.754-6.926-20.918-13.926-29.463c-1.422-1.794-2.909-3.39-4.535-5.009 c-12.454-12.52-29.778-19.701-47.531-19.701H67.244c-17.951,0-34.834,7-47.539,19.708c-1.608,1.604-3.099,3.216-4.575,5.067 c-6.97,8.509-11.747,18.659-13.824,29.428C0.438,114.62,0,119.002,0,123.435v265.137c0,9.224,1.874,18.206,5.589,26.745 c3.215,7.583,8.093,14.772,14.112,20.788c1.516,1.509,3.022,2.901,4.63,4.258c12.034,9.966,27.272,15.45,42.913,15.45h377.51 c15.742,0,30.965-5.505,42.967-15.56c1.604-1.298,3.091-2.661,4.578-4.148c5.818-5.812,10.442-12.49,13.766-19.854l0.438-1.05 c3.646-8.377,5.497-17.33,5.497-26.628V123.435C512,119.06,511.578,114.649,510.746,110.361z M34.823,99.104 c0.951-1.392,2.165-2.821,3.714-4.382c7.689-7.685,17.886-11.914,28.706-11.914h377.51c10.915,0,21.115,4.236,28.719,11.929 c1.313,1.327,2.567,2.8,3.661,4.272l2.887,3.88l-201.5,175.616c-6.212,5.446-14.21,8.443-22.523,8.443 c-8.231,0-16.222-2.99-22.508-8.436L32.19,102.939L34.823,99.104z M26.755,390.913c-0.109-0.722-0.134-1.524-0.134-2.341V128.925 l156.37,136.411L28.199,400.297L26.755,390.913z M464.899,423.84c-6.052,3.492-13.022,5.344-20.145,5.344H67.244 c-7.127,0-14.094-1.852-20.142-5.344l-6.328-3.668l159.936-139.379l17.528,15.246c10.514,9.128,23.922,14.16,37.761,14.16 c13.89,0,27.32-5.032,37.827-14.16l17.521-15.253L471.228,420.18L464.899,423.84z M485.372,388.572 c0,0.803-0.015,1.597-0.116,2.304l-1.386,9.472L329.012,265.409l156.36-136.418V388.572z"></path> </g> </g></svg>`
  },
  {
    titulo: "Teléfono",
    descripcion: "916 123 456",
    icono: `<svg version="1.0" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="64px" height="64px" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <g> <path fill="#394240" d="M59,29H47c-1.657,0-3,1.344-3,3s1.343,3,3,3h12c1.657,0,3-1.344,3-3S60.657,29,59,29z M59,33H47 c-0.553,0-1-0.447-1-1s0.447-1,1-1h12c0.553,0,1,0.447,1,1S59.553,33,59,33z"></path> <path fill="#394240" d="M57.312,44.57l-11.275-4.104c-1.559-0.566-3.279,0.236-3.846,1.793c-0.566,1.556,0.235,3.277,1.793,3.845 l11.276,4.105c1.558,0.565,3.278-0.238,3.845-1.793C59.672,46.858,58.87,45.137,57.312,44.57z M57.227,47.731 c-0.189,0.521-0.763,0.786-1.281,0.598l-11.277-4.104c-0.52-0.189-0.786-0.762-0.598-1.281c0.189-0.52,0.763-0.788,1.282-0.599 l11.276,4.104C57.148,46.638,57.416,47.212,57.227,47.731z"></path> <path fill="#394240" d="M46.036,23.53l11.276-4.104c1.557-0.566,2.359-2.289,1.793-3.843c-0.566-1.558-2.288-2.362-3.846-1.796 l-11.275,4.105c-1.559,0.566-2.36,2.289-1.794,3.846C42.757,23.294,44.479,24.098,46.036,23.53z M44.668,19.772l11.276-4.104 c0.519-0.188,1.093,0.079,1.281,0.599c0.189,0.52-0.078,1.092-0.597,1.281l-11.277,4.104c-0.52,0.189-1.093-0.078-1.281-0.598 C43.881,20.534,44.148,19.962,44.668,19.772z"></path> <path fill="#394240" d="M30,0H14c-0.52,0-1.039,0.105-1.527,0.309c-0.492,0.203-0.938,0.496-1.309,0.871C10.766,1.578,2,12,2,32 s8.316,29.688,8.672,30.219c0.113,0.172,0.262,0.309,0.395,0.457c0.062,0.066,0.109,0.137,0.176,0.199 c0.328,0.316,0.699,0.562,1.102,0.742c0.086,0.043,0.176,0.07,0.266,0.102C13.055,63.883,13.52,64,13.992,64 C13.996,64,14,64,14,64h12h4c2.211,0,4-1.789,4-4V46c0-2.211-1.789-4-4-4h-9c-0.553,0-1-0.447-1-1V23c0-0.553,0.447-1,1-1h9 c2.211,0,4-1.789,4-4V4C34,1.789,32.211,0,30,0z M26,44h4c1.103,0,2,0.897,2,2v14c0,1.103-0.897,2-2,2h-4V44z M22,44h2v18h-2V44z M20,20c-1.104,0-2,0.896-2,2v20c0,1.104,0.896,2,2,2v18h-6.008c-0.175,0-0.407-0.053-0.726-0.17l-0.09-0.031 c-0.006-0.002-0.012-0.005-0.018-0.008c-0.18-0.08-0.338-0.183-0.493-0.323c-0.04-0.047-0.08-0.095-0.123-0.141 c-0.045-0.05-0.091-0.1-0.138-0.148c-0.03-0.032-0.062-0.063-0.071-0.072c-0.031-0.047-0.099-0.14-0.203-0.274 C9.687,57.643,4,48.447,4,32c0-19.467,8.524-29.346,8.586-29.414c0.179-0.18,0.397-0.324,0.655-0.431C13.488,2.053,13.744,2,14,2 h6V20z M24,20h-2V2h2V20z M32,18c0,1.103-0.897,2-2,2h-4V2h4c1.103,0,2,0.897,2,2V18z"></path> </g> <g> <path fill="#f99080" d="M30,2h-4v18h4c1.103,0,2-0.897,2-2V4C32,2.897,31.103,2,30,2z"></path> <rect x="22" y="2" fill="#f99080" width="2" height="18"></rect> <path fill="#f99080" d="M30,44h-4v18h4c1.103,0,2-0.897,2-2V46C32,44.897,31.103,44,30,44z"></path> <rect x="22" y="44" fill="#f99080" width="2" height="18"></rect> <path fill="#f99080" d="M13.241,2.155c-0.258,0.106-0.477,0.251-0.655,0.431C12.524,2.654,4,12.533,4,32 c0,16.447,5.687,25.643,8.131,28.832c0.104,0.135,0.172,0.228,0.203,0.274c0.01,0.009,0.041,0.04,0.071,0.072 c0.047,0.049,0.093,0.099,0.138,0.148c0.043,0.046,0.083,0.094,0.123,0.141c0.155,0.141,0.313,0.243,0.493,0.323 c0.006,0.003,0.012,0.006,0.018,0.008l0.09,0.031c0.318,0.117,0.551,0.17,0.726,0.17H20V44c-1.104,0-2-0.896-2-2V22 c0-1.104,0.896-2,2-2V2h-6C13.744,2,13.488,2.053,13.241,2.155z"></path> </g> <g> <rect x="22" y="2" fill="#F9EBB2" width="2" height="18"></rect> <rect x="22" y="44" fill="#F9EBB2" width="2" height="18"></rect> </g> <g opacity="0.2"> <path d="M30,2h-4v18h4c1.103,0,2-0.897,2-2V4C32,2.897,31.103,2,30,2z"></path> <path d="M30,44h-4v18h4c1.103,0,2-0.897,2-2V46C32,44.897,31.103,44,30,44z"></path> </g> <g> <path fill="#B4CCB9" d="M45.352,21.651l11.277-4.104c0.519-0.189,0.786-0.762,0.597-1.281c-0.188-0.52-0.763-0.787-1.281-0.599 l-11.276,4.104c-0.52,0.189-0.787,0.762-0.598,1.281C44.259,21.573,44.832,21.841,45.352,21.651z"></path> <path fill="#B4CCB9" d="M59,31H47c-0.553,0-1,0.447-1,1s0.447,1,1,1h12c0.553,0,1-0.447,1-1S59.553,31,59,31z"></path> <path fill="#B4CCB9" d="M56.629,46.449l-11.276-4.104c-0.52-0.189-1.093,0.079-1.282,0.599c-0.188,0.52,0.078,1.092,0.598,1.281 l11.277,4.104c0.519,0.188,1.092-0.077,1.281-0.598C57.416,47.212,57.148,46.638,56.629,46.449z"></path> </g> </g> </g></svg>`
  },
  {
    titulo: "Horario",
    descripcion: "Lun-Vie: 9:00-18:00",
    icono: `<svg version="1.0" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="64px" height="64px" viewBox="0 0 64 64" enable-background="new 0 0 64 64" xml:space="preserve" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <circle fill="#ededed" cx="32" cy="32" r="26"></circle> <path fill="#394240" d="M32,0C14.327,0,0,14.327,0,32s14.327,32,32,32s32-14.327,32-32S49.673,0,32,0z M32,62 C15.431,62,2,48.568,2,32C2,15.431,15.431,2,32,2s30,13.431,30,30C62,48.568,48.569,62,32,62z"></path> <circle fill="#394240" cx="32" cy="32" r="1"></circle> <path fill="#394240" d="M36.931,32.688C36.962,32.461,37,32.236,37,32c0-1.631-0.792-3.064-2-3.978V14c0-1.657-1.343-3-3-3 s-3,1.343-3,3v14.022c-1.208,0.913-2,2.347-2,3.978c0,2.762,2.238,5,5,5c0.235,0,0.461-0.038,0.688-0.069l8.505,8.505 c1.172,1.172,3.07,1.171,4.242-0.001s1.172-3.07,0-4.242L36.931,32.688z M31,14c0-0.553,0.447-1,1-1s1,0.447,1,1v13.101 C32.677,27.035,32.343,27,32,27s-0.677,0.035-1,0.101V14z M29,32c0-1.657,1.343-3,3-3s3,1.343,3,3s-1.343,3-3,3S29,33.657,29,32z M44.021,44.021c-0.391,0.392-1.023,0.392-1.414,0.001l-7.853-7.853c0.562-0.372,1.043-0.853,1.415-1.415l7.852,7.853 C44.411,42.997,44.411,43.63,44.021,44.021z"></path> <path fill="#394240" d="M32,4C16.536,4,4,16.536,4,32s12.536,28,28,28s28-12.536,28-28S47.464,4,32,4z M51.075,49.66l-2.103-2.104 c-0.393-0.39-1.025-0.39-1.415,0c-0.391,0.392-0.391,1.023,0,1.415l2.104,2.104c-4.409,4.085-10.235,6.657-16.66,6.9l0.001-2.974 c-0.002-0.553-0.449-1-1-1c-0.554,0.001-1,0.447-1,1l-0.001,2.974c-6.425-0.243-12.251-2.814-16.66-6.898l2.104-2.104 c0.39-0.392,0.39-1.024,0-1.414c-0.393-0.391-1.023-0.391-1.414,0l-2.104,2.104c-4.084-4.409-6.656-10.235-6.9-16.66h2.974 c0.553-0.001,1-0.448,1-1c-0.001-0.554-0.447-1-1-1H6.025c0.243-6.425,2.814-12.252,6.898-16.661l2.104,2.104 c0.391,0.391,1.023,0.391,1.414,0c0.391-0.392,0.391-1.023,0-1.414l-2.104-2.104c4.409-4.085,10.236-6.657,16.661-6.9V9 c0,0.553,0.447,1,1,1s1-0.447,1-1V6.025c6.425,0.243,12.252,2.814,16.661,6.899l-2.104,2.104c-0.391,0.391-0.391,1.023,0,1.414 s1.023,0.391,1.414,0l2.105-2.104c4.084,4.409,6.656,10.236,6.899,16.661H55c-0.553,0-1,0.447-1,1s0.447,1,1,1h2.975 C57.731,39.425,55.16,45.251,51.075,49.66z"></path> <path fill="#F9EBB2" d="M32,2C15.432,2,2,15.432,2,32s13.432,30,30,30s30-13.432,30-30S48.568,2,32,2z M32,60 C16.536,60,4,47.464,4,32S16.536,4,32,4s28,12.536,28,28S47.464,60,32,60z"></path> <path fill="#F9EBB2" d="M32,29c-1.657,0-3,1.343-3,3s1.343,3,3,3s3-1.343,3-3S33.657,29,32,29z M32,33c-0.553,0-1-0.447-1-1 s0.447-1,1-1s1,0.447,1,1S32.553,33,32,33z"></path> <g> <path fill="#45AAB8" d="M44.021,42.606l-7.852-7.853c-0.372,0.562-0.854,1.043-1.414,1.414l7.853,7.854 c0.195,0.195,0.45,0.293,0.706,0.293s0.512-0.098,0.707-0.294C44.411,43.63,44.411,42.997,44.021,42.606z"></path> <path fill="#45AAB8" d="M32,13c-0.553,0-1,0.447-1,1v13.101C31.323,27.035,31.657,27,32,27s0.677,0.035,1,0.101V14 C33,13.447,32.553,13,32,13z"></path> </g> </g> </g></svg>`
  }
]);

const camposForm = ref({
  nombre: '',
  email: '',
  asunto: '',
  mensaje: ''
});

const enviarFormulario = () => {
  // Aquí iría la lógica para enviar el formulario
  console.log('Formulario enviado:', camposForm.value);
  // Resetear formulario
  camposForm.value = {
    nombre: '',
    email: '',
    asunto: '',
    mensaje: ''
  };
};
</script>

<template>
  <main class="contacto">
    <!-- Sección cabecera --> 
    <section class="cabecera">
      <div class="cabecera-contenido">
        <h1 class="cabecera-titulo">{{ tituloH1 }}</h1>
        <p class="cabecera-descripcion">{{ descripcion }}</p>
      </div>
    </section>
    
    <!-- Información de Contacto -->
    <section class="info">
      <div class="info-contenedor">
        <div v-for="(item, index) in contactInfo" 
             :key="index" 
             class="info-card">
          <div class="info-card-icono" v-html="item.icono"></div>
          <h3 class="info-card-titulo">{{ item.titulo }}</h3>
          <p class="info-card-descripcion">{{ item.descripcion }}</p>
        </div>
      </div>
    </section>

    <!-- Formulario de Contacto -->
    <section class="formulario-section">
      <div class="formulario-container">
        <h2 class="formulario-titulo">Envíanos un mensaje</h2>
        <form @submit.prevent="enviarFormulario" class="formulario">
          <div class="formulario-grupo">
            <label for="nombre">Nombre</label>
            <input 
              type="text" 
              id="nombre"
              v-model="camposForm.nombre"
              required
              placeholder="Tu nombre"
            >
          </div>

          <div class="formulario-grupo">
            <label for="email">Email</label>
            <input 
              type="email" 
              id="email"
              v-model="camposForm.email"
              required
              placeholder="tu@email.com"
            >
          </div>

          <div class="formulario-grupo">
            <label for="asunto">Asunto</label>
            <input 
              type="text" 
              id="asunto"
              v-model="camposForm.asunto"
              required
              placeholder="Asunto del mensaje"
            >
          </div>

          <div class="formulario-grupo">
            <label for="mensaje">Mensaje</label>
            <textarea 
              id="mensaje"
              v-model="camposForm.mensaje"
              required
              placeholder="Tu mensaje"
              rows="5"
            ></textarea>
          </div>

          <button type="submit" class="boton btn-primario">
            Enviar Mensaje
          </button>
        </form>
      </div>
    </section>

    <!-- Ubicación -->
    <section class="ubicacion">
      <div class="ubicacion-contenido">
        <h2 class="ubicacion-titulo">Nuestra Ubicación</h2>
        <p class="ubicacion-descripcion">
          Visítanos en nuestras oficinas centrales
        </p>
        <div class="ubicacion-info">
          <p class="ubicacion-direccion">
            Calle Ascao 29, 28017 Madrid
          </p>
          <p class="ubicacion-adicional">
            Metro: Línea 7 - Ascao
          </p>
        </div>
      </div>
    </section>
  </main>
</template>

<style scoped>
.contacto {
  background-color: #ffffff;
}

/* cabecera Section */
.cabecera {
  background: linear-gradient(45deg, var(--color-terciario), var(--color-resaltar2));
  min-height: 40vh;
  display: flex;
  align-items: center;
  color: white;
  padding: 4rem 2rem;
}

.cabecera-contenido {
  color: var(--color-texto-secundario);
  max-width: 800px;
  margin: 0 auto;
  text-align: center;
}

.cabecera-titulo {
  font-size: 3rem;
  font-weight: 800;
  margin-bottom: 1.5rem;
  line-height: 1.2;
}

.cabecera-descripcion {
  font-size: 1.25rem;
  line-height: 1.6;
  opacity: 0.9;
}

/* Información de Contacto */
.info {
  padding: 4rem 2rem;
  background-color: #f8f9fa;
}

.info-contenedor {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
  max-width: 1200px;
  margin: 0 auto;
}

.info-card {
  flex: 1;
  min-width: 250px;
  text-align: center;
  padding: 2rem;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.info-card:hover {
  transform: translateY(-1px);
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
}

.info-card-icono {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.info-card-titulo {
  font-size: 1.25rem;
  margin-bottom: 0.5rem;
  color: #333;
}

.info-card-descripcion {
  color: #666;
}

/* Formulario Section */
.formulario-section {
  padding: 4rem 2rem;
  background-color: white;
}

.formulario-container {
  max-width: 600px;
  margin: 0 auto;
}

.formulario-titulo {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 2rem;
  color: #333;

  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);
}

.formulario {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.formulario-grupo {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.formulario-grupo label {
  font-weight: 600;
  color: #333;
}

.formulario-grupo input,
.formulario-grupo textarea {
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
}

.formulario-grupo input:focus,
.formulario-grupo textarea:focus {
  outline: none;
  border-color: var(--color-secundario);
  box-shadow: 0 0 0 2px rgba(120,4,148,0.1);
}

/* Ubicación Section */
.ubicacion {
  padding: 4rem 2rem;
  background-color: #f8f9fa;
}

.ubicacion-contenido {
  max-width: 800px;
  margin: 0 auto;
  text-align: center;
  padding: 2rem;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.ubicacion-titulo {
  font-size: 2rem;
  margin-bottom: 1rem;

  color: var(--color-primario);
 
}

.ubicacion-descripcion {
  margin-bottom: 2rem;
  color: #666;
}

.ubicacion-info {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.ubicacion-direccion {
  font-size: 1.1rem;
  color: #333;
  font-weight: 600;
}

.ubicacion-adicional {
  color: #666;
}

/* Botones */
.boton {
  display: inline-block;
  padding: 1rem 2rem;
  border: none;
  border-radius: 4px;
  font-size: medium;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s ease;
}

.btn-primario {
  background-color: var(--color-secundario);
  color: var(--color-fondo);
}

.btn-primario:hover {
  background-color: var(--color-resaltar);;
  transform: translateY(-2px);
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
}

/* Responsive */
@media (max-width: 768px) {
  .cabecera-titulo {
    font-size: 2rem;
  }

  .cabecera-descripcion {
    font-size: 1rem;
  }

  .info-card {
    min-width: 100%;
  }

  .formulario-section {
    padding: 2rem 1rem;
  }
}
</style>