<script setup>
import { ref} from 'vue';

const props = defineProps({
  usuario: {
    type: Object,
    required: true
  }
});

const emit = defineEmits(['close', 'save']);

// Crear una copia local del usuario para editar
const usuarioLocal = ref({
  Id: props.usuario.Id,
  Nombre: props.usuario.Nombre,
  Email: props.usuario.Email,
  TipoUsuario: props.usuario.TipoUsuario
});

// Validaciones
const errores = ref({
  Nombre: '',
  Email: '',
});

function validarFormulario() {
  let isValid = true;
  errores.value = {
    Nombre: '',
    Email: '',
  };

  // Validar Nombre
  if (!usuarioLocal.value.Nombre.trim()) {
    errores.value.Nombre = 'El nombre es obligatorio';
    isValid = false;
  }

  // Validar Email
  if (!usuarioLocal.value.Email) {
    errores.value.Email = 'El email es obligatorio';
    isValid = false;
  } else if (!validarEmail(usuarioLocal.value.Email)) {
    errores.value.Email = 'El email no es válido';
    isValid = false;
  }

  return isValid;
}

function validarEmail(email) {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(email);
}

function guardarCambios() {
  if (validarFormulario()) {
    emit('save', usuarioLocal.value);
  }
}
</script>

<template>
  <div class="modal-fondo">
    <div class="modal">
      <div class="modal-header">
        <h3>Editar Usuario</h3>
        <button class="btn-cerrrar" @click="$emit('close')">×</button>
      </div>

      <div class="modal-body">
        <form @submit.prevent="guardarCambios">

          <!-- Campo Nombre -->
          <div class="modal-form-group">
            <label class="modal-form-label" for="nombre">Nombre:</label>
            <input 
              type="text" 
              id="nombre"
              v-model="usuarioLocal.Nombre"
              class="modal-form-input"
              :class="{ 'error': errores.Nombre }"
            >
            <span class="error-text" v-if="errores.Nombre">{{ errores.Nombre }}</span>
          </div>

          <!-- Campo Email -->
          <div class="modal-form-group">
            <label class="modal-form-label" for="email">Email:</label>
            <input 
              type="email" 
              id="email"
              v-model="usuarioLocal.Email"
              class="modal-form-input"
              :class="{ 'error': errores.Email }"
            >
            <span class="error-text" v-if="errores.Email">{{ errores.Email }}</span>
          </div>

          <!-- Selector Tipo Usuario -->
          <div class="modal-form-group">
            <label class="modal-form-label" for="tipo">Tipo de Usuario:</label>
            <select 
              id="tipo"
              v-model="usuarioLocal.TipoUsuario"
              class="modal-form-input"
            >
              <option value="1">Administrador</option>
              <option value="2">Usuario</option>
            </select>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn-cancelar" @click="$emit('close')">Cancelar</button>
            <button type="submit" class="btn-guardar">Guardar Cambios</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
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

.modal {
  background: white;
  border-radius: 8px;
  padding: 20px;
  max-width: 500px;
  width: 90%;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  padding-bottom: 10px;
  border-bottom: 1px solid #eee;
}

.modal-header h3 {
  font-size: 1.5rem;
  margin: 0;
  font-weight: bold;
  color: var(--color-primario);
}

.btn-cerrrar {
  border: none;
  background: none;
  font-size: 24px;
  cursor: pointer;
  color: #666;
}

.btn-cerrrar:hover {
  color: #dc3545;
}

/* Formulario */
.modal-form-label {
  display: block;
  margin-bottom: 5px;
  font-weight: 600;
  color: #333;
}

.modal-form-input {
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
  margin-bottom: 20px;

}

/* Estilos de error */
.modal-form.error {
  border-color: #dc3545;
}

.error-text {
  color: #dc3545;
  font-size: 12px;
  margin-top: 5px;
  display: block;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
  padding-top: 15px;
  border-top: 1px solid #eee;
}

.btn-cancelar, 
.btn-guardar {
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: 500;
  transition: background-color 0.3s;
}

.btn-cancelar {
  background-color: #6c757d;
  color: white;
}

.btn-guardar {
  background-color: var(--color-secundario);
  color: white;
}

.btn-cancelar:hover {
  background-color: #5a6268;
}

.btn-guardar:hover {
  background-color: rgb(3, 156, 115);
}


/* Responsivew */
@media (max-width: 768px) {
  .modal {
    width: 95%;
    margin: 10px;
  }

  .modal-footer {
    flex-direction: column;
  }

  .btn-cancelar, 
  .btn-guardar {
    width: 100%;
  }
}
</style>