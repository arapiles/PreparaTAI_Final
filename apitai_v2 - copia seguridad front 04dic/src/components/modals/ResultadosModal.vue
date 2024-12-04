<script setup>
// Props: propiedades que un componente padre pasa a un componente hijo
defineProps({
  resultados: {
    type: Object,
    required: true // es obligatorio pasar este prop
  }
});

// Definir eventos que emite el componente
const emit = defineEmits(['close']);

</script>

<template>
  <div class="modal">
    <div class="modal-fondo" @click="$emit('close')">
      <div class="modal-contenido">
        <h2 class="modal-titulo">Resultados del Examen</h2>

        <div class="resultados">
          <div class="resultados-item">
            <span class="resultados-numero">{{ resultados.aciertos }}</span>
            <span class="resultados-texto">Aciertos</span>
          </div>

          <div class="resultados-item">
            <span class="resultados-numero">{{ resultados.total }}</span>
            <span class="resultados-texto">Total Preguntas</span>
          </div>

          <div class="resultados-nota">
            <span class="resultados-numero resultados-numero-grande">
              {{ resultados.nota }}
            </span>
            <span class="resultados-texto">Nota Final</span>
          </div>

          <button class="btn-cerrar" @click="$emit('close')">
            Cerrar
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1000;
}

.modal-fondo {
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-contenido {
  background-color: var(--color-fondo);
  padding: 30px;
  border-radius: 8px;
  min-width: 300px;
  max-width: 500px;
  position: relative;
}

.modal-titulo {
  font-size: 24px;
  color: var(--color-primario);
  font-weight: bold;
  margin-bottom: 20px;
  text-align: center;
}


.btn-cerrar {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1em;
  transition: background-color 0.2s;
  min-width: 120px; 
    background-color: #6c757d;
  color: white;
}

.btn-cerrar:hover {
  background-color: #5a6268;
}

.resultados {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.resultados-item {
  text-align: center;
}

.resultados-numero {
  display: block;
  font-size: 24px;
  font-weight: bold;
  color: rgb(51, 92, 179);
}

.resultados-numero-grande {
  font-size: 48px;
}

.resultados-texto {
  color: #666;
  font-size: 14px;
  margin-top: 5px;
}

.resultados-nota {
  padding: 20px;
  background-color: var(--color-fondo2);
  border-radius: 8px;
  margin-top: 10px;
  text-align: center;
}

@media (max-width: 768px) {
  .modal-contenido {
    width: 90%;
    padding: 20px;
  }

  .resultados-numero-grande {
    font-size: 36px;
  }
}
</style>