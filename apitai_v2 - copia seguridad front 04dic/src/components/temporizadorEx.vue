<script setup>
import { ref, computed,onMounted, onUnmounted } from 'vue';
import Swal from 'sweetalert2';
import { swal } from 'sweetalert2/dist/sweetalert2';

// Props que recibe el componente
const props = defineProps({
  tiempoMinutos: {
    type: Number,
    required: true
  }
});

// Eventos que emite
const emit = defineEmits(['tiempoTerminado']);

// Estado del temporizador
const tiempoRestante = ref(props.tiempoMinutos * 60); // Convertir a segundos
let temporizador = null;

// Formatear tiempo 00:00
const tiempoFormateado = computed(() => {
  const minutos = Math.floor(tiempoRestante.value / 60);
  const segundos = tiempoRestante.value % 60;
  
  return `${minutos}:${segundos.toString().padStart(2, '0')}`;
});



// Iniciar temporizador cuando el componente se monte
onMounted(() => {
  temporizador = setInterval(() => {
    if (tiempoRestante.value > 0) {
      tiempoRestante.value--;

    } else {
      clearInterval(temporizador);
      emit('tiempoTerminado'); // CUando el tiempo acabe emitir evento

    }
  }, 1000);
});


// Limpiar temporizador al desmontar
onUnmounted(() => {
  if (temporizador) {
    clearInterval(temporizador);
  }
});

</script>

<template>
  <div class="temporizador">
    <div class="tiempo" :class="{ 'tiempo-bajo': tiempoRestante <= 60 }"> <!-- :class añade una clase dinamica cuando tiempoRestante sea de 1min para el parpadeo se podría cambiar por un evento -->
      {{ tiempoFormateado }}
    </div>
  </div>
</template>

<style scoped>
.temporizador {
  position: fixed;
  top: 100px;
  right: 50px;
  z-index: 1000;
}

.tiempo {
  background-color: var(--color-secundario);
  color: var(--color-fondo2);
  padding: 10px 20px;
  border-radius: 8px;
  font-size: 1.2rem;
  font-weight: bold;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
}

.tiempo-bajo {
  background: linear-gradient(45deg, #c62828, #ff5252);
  animation: parpadeo 1s infinite;
}

/* Animación -1min */
@keyframes parpadeo {
  50% {
    opacity: 0.5;
  }
}

</style>