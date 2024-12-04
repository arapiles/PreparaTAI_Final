<script setup>

// Props que recibe el componente
const props = defineProps({
  usuario: {
    type: Object,
    required: true
  }
});

// Eventos que emite el componente
const emit = defineEmits(['close']);

// Función formatear la fecha
function formatearFecha(fecha) {
  return new Date(fecha).toLocaleDateString('es-ES', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });
}

// Función traducir tipo de usuario
function getTipoUsuario(tipo) {
  switch(tipo) {
    case 1: return "Administrador";
    case 2: return "Usuario";
    default: return "Desconocido";
  }
}
</script>

<template>
  <div class="modal-fondo">
    <div class="modal">
      <div class="modal-header">
        <h3>Detalles del Usuario</h3>
        <button class="btn-cerrar" @click="$emit('close')">×</button>
      </div>

      <div class="modal-cuerpo">
        <div class="detalle-grupo">
          <label>ID:</label>
          <span>{{ usuario.Id }}</span>
        </div>

        <div class="detalle-grupo">
          <label>Nombre:</label>
          <span>{{ usuario.Nombre }}</span>
        </div>

        <div class="detalle-grupo">
          <label>Email:</label>
          <span>{{ usuario.Email }}</span>
        </div>

        <div class="detalle-grupo">
          <label>Tipo de Usuario:</label>
          <span>{{ getTipoUsuario(usuario.TipoUsuario) }}</span>
        </div>

        <div class="detalle-grupo">
          <label>Fecha de Registro:</label>
          <span>{{ formatearFecha(usuario.FechaRegistro) }}</span>
        </div>

        <div class="detalle-grupo">
          <label>Total de Resultados:</label>
          <span>{{ usuario.ResultadosCount || 0 }} exámenes realizados</span>
        </div>
      </div>

      <div class="modal-footer">
        <button class="btn-cerrar-modal" @click="$emit('close')">Cerrar</button>
      </div>
    </div>
  </div>
</template>

<style scoped>


.modal {
  background: var(--color-fondo2);
  border-radius: 8px;
  padding: 20px;
  max-width: 500px;
  width: 90%;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.modal-fondo {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* Fondo oscuro */
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
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

.btn-cerrar {
  border: none;
  background: none;
  font-size: 24px;
  cursor: pointer;
  color: #666;
}
.btn-cerrar:hover {
  
  color:rgb(251, 118, 118);
}

.modal-cuerpo {
  margin-bottom: 20px;
}

.detalle-grupo {
  margin-bottom: 15px;
  display: flex;
  gap: 10px;
}

.detalle-grupo label {
  font-weight: 600;
  min-width: 150px;
  color: #666;
}

.detalle-grupo span {
  color: #333;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  padding-top: 15px;
  border-top: 1px solid #eee;
}

.btn-cerrar-modal {
  background-color: #6c757d;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn-cerrar-modal:hover {
  background-color: #5a6268;
}

@media (max-width: 768px) {
  .modal {
    width: 95%;
    margin: 10px;
  }

  .detalle-grupo {
    flex-direction: column;
    gap: 5px;
  }

  .detalle-grupo label {
    min-width: auto;
  }
}
</style>