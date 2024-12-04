<script setup>
import barraMenu from "./components/menu.vue";
import barraFooter from "./components/footer.vue";
import { ref } from "vue";

let usuario = ref("");
usuario.value = localStorage.getItem("usuario"); 
</script>

<template>
  <header>
    <barraMenu/>
    <!--USUARIO NOLogin pags: Inicio, Examen, Tai, Nosotros, Contacto, Login -->
    <!--USUARIO Login pags: Inicio, Examen, Tai, Nosotros, Contacto, Usuario -->
    <!--ADMIN pags: Dashboard-->
    <main class="main-content">
    </main>
  </header>

  <main>
    <RouterView />
  </main>

  <barraFooter />
</template>

<style >
:root {
  /* Paleta de Colores - Modo Claro */
  --color-primario: #273c66;         
  --color-secundario: #0eb582;  
  --color-terciario: #96dbc6;
  --color-resaltar: #f5c84e;       
  --color-resaltar2: #fbda7f;         
  --color-fondo: #FFFFFF;
  --color-fondo2: #f5f5f5;              
  --color-texto-principal: #141f34;  
  --color-texto-secundario: #32405c; 

    

  /* Tipografía */
  --fuente-primaria: 'Montserrat', sans-serif; /* Encabezados h* */
  --fuente-secundaria: 'Merriweather', serif; /* Textos */
  --fuente-enfasis: 'Pacifico', cursive; /* Elementos destacados */
}

/* Paleta de Colores - Modo Oscuro ...en pruebas
@media (prefers-color-scheme: dark) {
  :root {
  --color-primario: #273c66;         
  --color-secundario: #0eb582;  
  --color-terciario: #96dbc6;
  --color-resaltar: #f5c84e;       
  --color-resaltar2: #fbda7f;         
  --color-fondo: #141f34;
  --color-fondo2: #32405c;              
  --color-texto-principal: #FFFFFF;  
  --color-texto-secundario: #f5f5f5; 
  }
}*/

.main-content {
  padding-top: 56px; /* Ajustar menu */
}
</style>

<!--
Comparacion REACT VS VUE
// React - useEffect se usa para todo tipo de efectos
useEffect(() => {
  // Este código se ejecuta al montar y cuando count cambia
  console.log('count cambió:', count)
}, [count])

// Vue es más específico en sus hooks de ciclo de vida
onMounted(() => {
  // Solo al montar
  console.log('componente montado')
})

watchEffect(() => {
  // Similar a useEffect - se ejecuta cuando las dependencias cambian
  console.log('count cambió:', count.value)
})

watch(count, (newValue, oldValue) => {
  // Más específico - solo observa count
  console.log('count cambió de', oldValue, 'a', newValue)
})
-->