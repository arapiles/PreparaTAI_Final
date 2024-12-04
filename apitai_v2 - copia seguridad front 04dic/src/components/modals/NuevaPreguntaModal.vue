<script setup>
import { ref } from 'vue';

// Eventos personalizado para el modal
const emit = defineEmits(['close', 'save']);

const nuevaPregunta = ref({
  Pregunta: '',
  Opcion1: '',
  Opcion2: '',
  Opcion3: '',
  Opcion4: '',
  Respuesta: '',
  Bloque: '',
  Tema: '',
  Origen: ''
});

const guardarPregunta = () => {
  emit('save', nuevaPregunta.value);
  nuevaPregunta.value = {
    Pregunta: '',
    Opcion1: '',
    Opcion2: '',
    Opcion3: '',
    Opcion4: '',
    Respuesta: '',
    Bloque: '',
    Tema: '',
    Origen: ''
  };
};
</script>

<template>
  <div class="modal">
    <div class="modal-fondo" @click="$emit('close')">
      <div class="modal-contenedor" @click.stop>
        <div class="modal-header">
          <h3 class="modal-titulo">Nueva Pregunta</h3>
          <button class="btn-cerrar" @click="$emit('close')">×</button>
        </div>
        
        <div class="modal-body">
          <form class="modal-form" @submit.prevent="guardarPregunta">
            <div class="modal-form-group">
              <label class="modal-form-label">Pregunta:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Pregunta" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Opción 1:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Opcion1" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Opción 2:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Opcion2" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Opción 3:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Opcion3" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Opción 4:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Opcion4" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Respuesta:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Respuesta" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Bloque:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Bloque" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Tema:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Tema" 
                required 
              />
            </div>
            
            <div class="modal-form-group">
              <label class="modal-form-label">Origen:</label>
              <input 
                class="modal-form-input" 
                type="text" 
                v-model="nuevaPregunta.Origen" 
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
                Guardar
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.modal {
  position: relative;
}

.modal-fondo {
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
  margin-top:80px;
  margin-bottom: 10px;
  background-color: white;
  border-radius: 8px;
  padding: 20px;
  width: 90%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  padding-bottom: 10px;
  border-bottom: 1px solid #eee;
}

.modal-titulo {
  font-size: 1.5rem;
  margin: 0;
  font-weight: bold;
  color: var(--color-primario);
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

.modal-body {
  margin-bottom: 20px;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  padding-top: 20px;
  border-top: 1px solid #eee;
}

/* Botones */
.btn-cancelar,.btn-guardar {
  padding: 8px 16px;
  border-radius: 4px;
  border: none;
  cursor: pointer;
  font-weight: 500;
  transition: background-color 0.3s ease;
}

.btn-cancelar {
  background-color: #6c757d;
  color: white;
}

.btn-cancelar:hover {
  background-color: #5a6268;
}

.btn-guardar {
  background-color: #273c66;
  color: white;
}

.btn-guardar:hover {
  background-color: #4061a3;
}

/* Form */
.modal-form {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.modal-form-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.modal-form-label {
  font-weight: 500;
  color: #333;
  font-size: 0.9rem;
}

.modal-form-input {
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

.modal-form-input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0,123,255,0.25);
}

.modal-form-input:hover {
  border-color: #bbbbbb;
}

/* Responsive */
@media (max-width: 768px) {
  .modal-contenedor {
    width: 95%;
    padding: 15px;
  }

  .modal-titulo {
    font-size: 1.2rem;
  }

  .modal-form-input {
    font-size: 16px; /* Previene zoom en iOS */
  }
}
</style>