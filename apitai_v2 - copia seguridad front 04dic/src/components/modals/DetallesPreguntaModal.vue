<script setup>

defineProps({
  pregunta: {
    type: Object,
    required: true
  }
});

// Eventos que emite el componente
defineEmits(['close', 'edit']);

</script>

<template>
  <div class="modal">
    <div class="modal-fondo">
      <div class="modal-contenedor">
        <div class="modal-header">
          <h2>Detalles de la Pregunta</h2>
          <button class="btn-cerrar" @click="$emit('close')">&times;</button>
        </div>

        <div class="modal-cuerpo">
          <div class="grupo-detalles">
            <h3>Pregunta:</h3>
            <p>{{ pregunta.Pregunta }}</p>
          </div>

          <div class="grupo-detalles">
            <h3>Opciones:</h3>
            <ul class="lista-opciones">
              <li> {{ pregunta.Opcion1 }}</li>
              <li> {{ pregunta.Opcion2 }}</li>
              <li> {{ pregunta.Opcion3 }}</li>
              <li> {{ pregunta.Opcion4 }}</li>
            </ul>
          </div>

          <div class="grupo-detalles">
            <h3>Respuesta Correcta:</h3>
            <p>{{ pregunta.Respuesta }}</p>
          </div>

          <div class="grupo-detalles">
            <h3>Información Adicional:</h3>
            <p><strong>Bloque:</strong> {{ pregunta.Bloque }}</p>
            <p><strong>Tema:</strong> {{ pregunta.Tema }}</p>
            <p><strong>Origen:</strong> {{ pregunta.Origen }}</p>
          </div>
        </div>

        <div class="modal-footer">
          <button class="btn-cerrar-modal" @click="$emit('close')">
            Cerrar
          </button>
        </div>
      </div>
    </div>
  </div>
</template>



<style scoped>
.modal {
  position: relative;
  color: rgb(49, 49, 49);
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

.modal-cuerpo {
  padding: 1.5rem;
}

h2 {
  color: var(--color-primario);
  font-weight: bold;
}

.grupo-detalles {
  margin-bottom: 1.5rem;
}

.grupo-detalles h3 {
  font-weight: bold;
  color: var(--color-texto-secundario);
  margin-bottom: 0.5rem;
}

.lista-opciones {
  list-style: none;
  padding: 0;
}

.lista-opciones li {
  padding: 0.5rem 0;
  border-bottom: 1px solid #eee;
}

.modal-footer {
  padding: 1rem;
  border-top: 1px solid #dee2e6;
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
}

.btn-cerrar-modal {
  padding: 0.5rem 1rem;
  background-color: #6c757d;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.boton-editar {
  color: white;
  padding: 0.5rem 1rem;
  background-color: #ffc107;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.btn-cerrar {
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
  color: #6c757d;
}

.btn-cerrar:hover {
  color: #cd0000;
}
</style>