<script setup>
import { ref, onMounted } from 'vue';

// Necesita props para recibir la pregunta a editar
const props = defineProps({
  pregunta: {
    type: Object,
    required: true
  }
});

const emit = defineEmits(['close', 'save']);

// Crear una copia local de la pregunta para editar
const preguntaLocal = ref({ ...props.pregunta });

const guardarCambios = () => {
  emit('save', preguntaLocal.value);
};

/* Meter validaciones */
</script>

<template>
    <div class="modal">
      <div class="modal-contenedor">
        <div class="modal-header">
          <h2 class="modal-titulo">Editar Pregunta</h2>
          <button class="btn-cerrar" @click="$emit('close')">×</button>
        </div>
  
        <div class="modal-body">
          <form class="modal-formulario" @submit.prevent="guardarCambios">
            <div class="modal-form-conten">
              <label class="modal-form-label">Pregunta:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Pregunta"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Opción 1:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Opcion1"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Opción 2:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Opcion2"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Opción 3:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Opcion3"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Opción 4:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Opcion4"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Respuesta:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Respuesta"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Bloque:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Bloque"
                required
              />
            </div>
  
            <div class="modal-form-conten">
              <label class="modal-form-label">Tema:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Tema"
                required
              />
            </div>

            <div class="modal-form-conten">
              <label class="modal-form-label">Origen:</label>
              <input 
                type="text"
                class="modal-form-input"
                v-model="preguntaLocal.Origen"
                required
              />
            </div>
            
            <div class="modal-footer">
              <button 
                type="button"
                class="btn-cancelar"
                @click="$emit('close')"
              >
                Cancelar
              </button>
              <button 
                type="submit"
                class="btn-guardar"
              >
                Guardar Cambios
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  

  
<style scoped>
  .modal {
    color: #2e2e2e;
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }
  
  .modal-contenedor {
    margin-top: 100px;
    margin-bottom: 30px;
    background-color: white;
    border-radius: 8px;
    width: 90%;
    max-width: 600px;
    max-height: 90vh;
    overflow-y: auto;
  }
  
  .modal-header {
    padding: 1rem;
    border-bottom: 1px solid #dee2e6;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

 .modal-titulo{
  margin: 0;
  font-weight: bold;
  color: var(--color-primario);
 }
  
  .modal-formulario {
    padding: 1.5rem;
  }
  
  .modal-form-conten {
    margin-bottom: 1rem;
  }
  
  .modal-form-label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: 500;
  }
  
  .modal-form-input {
    width: 100%;
    padding: 0.5rem;
    border: 1px solid #ddd;
    border-radius: 4px;
    font-size: 1rem;
  }
  
  .modal-footer {
    padding-top: 1rem;
    border-top: 1px solid #dee2e6;
    display: flex;
    justify-content: flex-end;
    gap: 1rem;
  }
  
  /* Botones */
  .btn-cancelar {
    padding: 0.5rem 1rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    background-color: #6c757d;
    color: white;
  }
  
  .btn-guardar {
    padding: 0.5rem 1rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    background-color: var(--color-secundario);
    color: white;
  }
  
  .btn-guardar:hover {
    background-color: rgb(3, 156, 115);
  }
  
  .btn-cancelar:hover {
    background-color: #5a6268;
  }

  .btn-cerrar {
    background: none;
    border: none;
    font-size: 1.5rem;
    cursor: pointer;
    padding: 0;
    color: #666;
  }

  .btn-cerrar:hover {
  color: #cd0000;
}
</style>