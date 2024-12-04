<script setup>
import { ref} from 'vue';

const emit = defineEmits(['close', 'save']);

// Datos del nuevo usuario
const nuevoUsuario = ref({
  Nombre: '',
  Email: '',
  Contraseña: '',
  TipoUsuario: 2, // Por defecto será usuario normal
});

// Validaciones
const errores = ref({
  Nombre: '',
  Email: '',
  Contraseña: '',
});

function validarFormulario() {
  let isValid = true;
  errores.value = {
    Nombre: '',
    Email: '',
    Contraseña: '',
  };

  // Validar Nombre
  if (!nuevoUsuario.value.Nombre.trim()) {
    errores.value.Nombre = 'El nombre es obligatorio';
    isValid = false;
  }

  // Validar Email
  if (!nuevoUsuario.value.Email) {
    errores.value.Email = 'El email es obligatorio';
    isValid = false;
  } else if (!validarEmail(nuevoUsuario.value.Email)) {
    errores.value.Email = 'El email no es válido';
    isValid = false;
  }

  // Validar Contraseña
  if (!nuevoUsuario.value.Contraseña) {
    errores.value.Contraseña = 'La contraseña es obligatoria';
    isValid = false;
    
  } else if (nuevoUsuario.value.Contraseña.length < 6) {
    errores.value.Contraseña = 'La contraseña debe tener al menos 6 caracteres';
    isValid = false;
  }

  return isValid;
}

function validarEmail(email) {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return re.test(email);
}

function guardar() {
  if (validarFormulario()) {
    emit('save', nuevoUsuario.value);
  }
}
</script>

<template>
  <div class="modal-fondo">
    <div class="modal">
      <div class="modal-header">
        <h3>Nuevo Usuario</h3>
        <button class="btn-cerrar" @click="$emit('close')">×</button>
      </div>

      <div class="modal-body">
        <form @submit.prevent="guardar">
          <!-- Campo Nombre -->
          <div class="modal-form-group">
            <label for="nombre">Nombre:</label>
            <input 
              type="text" 
              id="nombre"
              v-model="nuevoUsuario.Nombre"
              class="modal-form-input"
              :class="{ 'error': errores.Nombre }"
            >
            <span class="error-text" v-if="errores.Nombre">{{ errores.Nombre }}</span>
          </div>

          <!-- Campo Email -->
          <div class="modal-form-group">
            <label for="email">Email:</label>
            <input 
              type="email" 
              id="email"
              v-model="nuevoUsuario.Email"
              class="modal-form-input"
              :class="{ 'error': errores.Email }"
            >
            <span class="error-text" v-if="errores.Email">{{ errores.Email }}</span>
          </div>

          <!-- Campo Contraseña -->
          <div class="modal-form-group">
            <label for="contraseña">Contraseña:</label>
            <input 
              type="password" 
              id="contraseña"
              v-model="nuevoUsuario.Contraseña"
              class="modal-form-input"
              :class="{ 'error': errores.Contraseña }"
            >
            <span class="error-text" v-if="errores.Contraseña">{{ errores.Contraseña }}</span>
            <span class="help-text">La contraseña debe tener al menos 6 caracteres</span>
          </div>

          <!-- Selector Tipo Usuario -->
          <div class="modal-form-group">
            <label for="tipo">Tipo de Usuario:</label>
            <select 
              id="tipo"
              v-model="nuevoUsuario.TipoUsuario"
              class="modal-form-input"
            >
              <option value="1">Administrador</option>
              <option value="2">Usuario</option>
            </select>
          </div>

          <div class="modal-footer">
            <button type="button" class="btn-cancelar" @click="$emit('close')">Cancelar</button>
            <button type="submit" class="btn-guardar">Crear Usuario</button>
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
  color: #273c66;
  font-weight: bold;
}

.btn-cerrar {
  border: none;
  background: none;
  font-size: 24px;
  cursor: pointer;
  color: #666;
}

.modal-form-group {
  margin-bottom: 20px;
}

.modal-form-group label {
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
}

.modal-form-input.error {
  border-color: #dc3545;
}

.error-text {
  color: #dc3545;
  font-size: 12px;
  margin-top: 5px;
  display: block;
}

.help-text {
  color: #6c757d;
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
  background-color: #273c66;
  color: white;
}

.btn-cancelar:hover {
  background-color: #5a6268;
}

.btn-guardar:hover {
  background-color: #375590;
}

select.modal-form-input {
  appearance: none;
  background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2' stroke-linecap='round' stroke-linejoin='round'%3e%3cpolyline points='6 9 12 15 18 9'%3e%3c/polyline%3e%3c/svg%3e");
  background-repeat: no-repeat;
  background-position: right 8px center;
  background-size: 1em;
}

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