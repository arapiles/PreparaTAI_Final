<script setup>
import { RouterLink } from 'vue-router';
</script>

<template>
  <RouterLink to="/" class="logotipo">PreparaTAI</RouterLink>
</template>

<style scoped>
.logotipo {

  height: 100%;
  font-size: 1.5rem;
  font-weight: 800;
  padding: 0.5rem;
  padding-bottom: 5px;
  padding-right: 25px;
  padding-left: 15px;
  color: var(--color-resaltar);
  background-color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 5px;
  text-decoration-color: var(--color-secundario);
  
}

@media (max-width: 768px) {
  .logotipo{
      display: none;
    }

  }
</style>
