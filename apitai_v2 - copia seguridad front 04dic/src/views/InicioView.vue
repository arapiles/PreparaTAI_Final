<script setup>
import { ref } from "vue";
import { RouterLink } from 'vue-router';

//const tituloH1 = ref("Bienvenido a PreparaTAI");
const descripcion = ref("Prepárate para tu oposición TAI con exámenes personalizados usando IA. ¡Tu éxito es nuestra prioridad!");

// Info para recorrer con el for en el template y llenar la card 
const elegirSection = ref([
  {
    titulo: "Exámenes Personalizados",
    descripcion: "Adaptamos las preguntas a tu nivel y necesidades",
    svg: `<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 512 512" xml:space="preserve" width="64px" height="64px" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path style="fill:#509FE8;" d="M427.023,503.983H84.977c-9.446,0-17.102-7.656-17.102-17.102V84.977 c0-9.446,7.656-17.102,17.102-17.102h342.046c9.446,0,17.102,7.656,17.102,17.102v401.904 C444.125,496.327,436.469,503.983,427.023,503.983z"></path> <path style="fill:#4D8CCF;" d="M84.977,486.881V84.977c0-9.445,7.656-17.102,17.102-17.102H84.977 c-9.446,0-17.102,7.658-17.102,17.102v401.904c0,9.445,7.656,17.102,17.102,17.102h17.102 C92.634,503.983,84.977,496.326,84.977,486.881z"></path> <rect x="110.63" y="67.875" style="fill:#F6F6F7;" width="290.739" height="393.353"></rect> <path style="fill:#D9DCDF;" d="M152.852,110.63h206.296c4.427,0,8.017-3.589,8.017-8.017V67.875h-222.33v34.739 C144.835,107.041,148.424,110.63,152.852,110.63z"></path> <path style="fill:#FFC44F;" d="M350.063,93.528V59.324c0-9.446-7.656-17.102-17.102-17.102h-42.756 c0-18.891-15.314-34.205-34.205-34.205s-34.205,15.314-34.205,34.205H179.04c-9.446,0-17.102,7.656-17.102,17.102v34.205H350.063z"></path> <path style="fill:#FFD791;" d="M332.96,42.221h-17.102c9.446,0,17.102,7.658,17.102,17.102v34.205h17.102V59.324 C350.063,49.879,342.406,42.221,332.96,42.221z"></path> <path style="fill:#F7AB49;" d="M196.142,42.221H179.04c-9.446,0-17.102,7.658-17.102,17.102v34.205h17.102V59.324 C179.04,49.879,186.696,42.221,196.142,42.221z"></path> <path style="fill:#FF8C78;" d="M356.808,316.761l-85.512-171.023c-2.897-5.794-8.818-9.454-15.297-9.454 c-6.479,0-12.4,3.66-15.297,9.454l-85.511,171.023c-4.224,8.447-0.8,18.721,7.649,22.945c8.445,4.223,18.721,0.799,22.945-7.649 l33.754-67.506h72.923l33.754,67.506c2.996,5.993,9.035,9.459,15.31,9.458c2.57,0,5.18-0.581,7.635-1.809 C357.608,335.482,361.033,325.209,356.808,316.761z M236.64,230.347L256,191.628l19.36,38.719H236.64z"></path> <path d="M427.023,59.858h-68.944v-0.534c0-13.851-11.268-25.119-25.119-25.119h-35.502C293.703,14.743,276.542,0,256,0 c-20.541,0-37.703,14.743-41.458,34.205H179.04c-13.851,0-25.119,11.268-25.119,25.119v0.534H84.977 c-13.851,0-25.119,11.268-25.119,25.119v273.637c0,4.427,3.589,8.017,8.017,8.017s8.017-3.589,8.017-8.017V84.977 c0-5.01,4.076-9.086,9.086-9.086h17.637v385.336c0,4.427,3.589,8.017,8.017,8.017H401.37c4.428,0,8.017-3.589,8.017-8.017v-299.29 c0-4.427-3.588-8.017-8.017-8.017s-8.017,3.589-8.017,8.017v291.273H118.647V75.891h35.273v17.637c0,4.427,3.589,8.017,8.017,8.017 h188.125c4.428,0,8.017-3.589,8.017-8.017V75.891h35.273v43.29c0,4.427,3.588,8.017,8.017,8.017s8.017-3.589,8.017-8.017v-43.29 h17.637c5.01,0,9.086,4.076,9.086,9.086v401.904c0,5.01-4.076,9.086-9.086,9.086H84.977c-5.01,0-9.086-4.076-9.086-9.086V401.37 c0-4.427-3.589-8.017-8.017-8.017s-8.017,3.589-8.017,8.017v85.511c0,13.851,11.268,25.119,25.119,25.119h342.046 c13.851,0,25.119-11.268,25.119-25.119V84.977C452.142,71.126,440.874,59.858,427.023,59.858z M342.046,85.511H169.954V59.324 c0-5.01,4.076-9.086,9.086-9.086h42.756c4.427,0,8.017-3.589,8.017-8.017c0-14.44,11.748-26.188,26.188-26.188 c14.441,0,26.188,11.748,26.188,26.188c0,4.427,3.588,8.017,8.017,8.017h42.756c5.01,0,9.086,4.076,9.086,9.086V85.511z"></path> <path d="M256,25.653c-4.427,0-8.017,3.589-8.017,8.017v8.551c0,4.427,3.589,8.017,8.017,8.017c4.428,0,8.017-3.589,8.017-8.017 V33.67C264.017,29.243,260.428,25.653,256,25.653z"></path> <path d="M352.746,346.876c12.388-6.195,17.427-21.313,11.233-33.7l-85.511-171.023c-4.282-8.565-12.891-13.886-22.467-13.886 c-9.575,0-18.185,5.321-22.467,13.886l-85.511,171.023c-6.194,12.387-1.155,27.506,11.234,33.7 c12.388,6.194,27.506,1.155,33.7-11.234l31.538-63.074h63.014l31.537,63.074C325.17,347.896,340.458,353.017,352.746,346.876z M333.385,328.472l-33.754-67.506c-1.359-2.716-4.133-4.432-7.17-4.432h-72.923c-3.037,0-5.813,1.716-7.17,4.432l-33.754,67.506 c-2.241,4.482-7.71,6.303-12.19,4.064c-4.481-2.24-6.304-7.709-4.064-12.19l85.511-171.023c3.286-6.572,12.968-6.572,16.254,0 l85.512,171.023l0,0c2.288,4.575,0.231,10.263-4.412,12.355C340.831,334.681,335.533,332.768,333.385,328.472z"></path> <path d="M263.17,188.043c-2.897-5.792-11.444-5.794-14.34,0l-19.359,38.719c-2.621,5.24,1.323,11.602,7.17,11.602h38.719 c5.847,0,9.792-6.362,7.17-11.602L263.17,188.043z M249.612,222.33L256,209.553l6.389,12.776H249.612z"></path> <path d="M332.96,128.267c-4.428,0-8.017,3.589-8.017,8.017v9.086h-9.086c-4.428,0-8.017,3.589-8.017,8.017 c0,4.427,3.588,8.017,8.017,8.017h9.086v9.086c0,4.427,3.588,8.017,8.017,8.017c4.428,0,8.017-3.589,8.017-8.017v-9.086h9.086 c4.428,0,8.017-3.589,8.017-8.017c0-4.427-3.588-8.017-8.017-8.017h-9.086v-9.086C340.977,131.857,337.389,128.267,332.96,128.267z"></path> <path d="M332.426,384.267c0-4.427-3.588-8.017-8.017-8.017H187.591c-4.427,0-8.017,3.589-8.017,8.017 c0,4.427,3.589,8.017,8.017,8.017h136.818C328.838,392.284,332.426,388.695,332.426,384.267z"></path> <path d="M187.591,410.455c-4.427,0-8.017,3.589-8.017,8.017c0,4.427,3.589,8.017,8.017,8.017h76.96c4.428,0,8.017-3.589,8.017-8.017 c0-4.427-3.588-8.017-8.017-8.017H187.591z"></path> </g></svg>`
  },
  {
    titulo: "Análisis Detallado",
    descripcion: "Seguimiento de tu progreso con estadísticas detalladas",
    svg: `<svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 512 512" xml:space="preserve" width="64px" height="64px" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <polygon style="fill:#509FE8;" points="290.205,503.983 221.795,503.983 230.347,324.409 281.653,324.409 "></polygon> <polygon style="fill:#4D8CCF;" points="281.653,324.409 230.347,324.409 225.036,435.93 286.964,435.93 "></polygon> <path style="fill:#D9DCDF;" d="M469.779,409.921H42.221c-18.891,0-34.205-15.314-34.205-34.205V42.221 c0-18.891,15.314-34.205,34.205-34.205h427.557c18.891,0,34.205,15.314,34.205,34.205v333.495 C503.983,394.607,488.669,409.921,469.779,409.921z"></path> <g> <path style="fill:#509FE8;" d="M8.017,358.614v17.102c0,18.891,15.314,34.205,34.205,34.205h427.557 c18.891,0,34.205-15.314,34.205-34.205v-17.102H8.017z"></path> <path style="fill:#509FE8;" d="M503.983,42.221c0-18.891-15.314-34.205-34.205-34.205H42.221 c-18.891,0-34.205,15.314-34.205,34.205v17.102h495.967V42.221z"></path> </g> <path style="fill:#B3B9BF;" d="M33.67,375.716V42.221c0-18.891,15.314-34.205,34.205-34.205H42.221 c-18.891,0-34.205,15.314-34.205,34.205v333.495c0,18.891,15.314,34.205,34.205,34.205h25.653 C48.984,409.921,33.67,394.607,33.67,375.716z"></path> <g> <path style="fill:#4D8CCF;" d="M33.67,375.716v-17.102H8.017v17.102c0,18.891,15.314,34.205,34.205,34.205h25.653 C48.984,409.921,33.67,394.607,33.67,375.716z"></path> <path style="fill:#4D8CCF;" d="M67.875,8.017H42.221c-18.891,0-34.205,15.314-34.205,34.205v17.102H33.67V42.221 C33.67,23.331,48.984,8.017,67.875,8.017z"></path> </g> <g> <rect x="50.772" y="196.142" style="fill:#FFC44F;" width="51.307" height="119.716"></rect> <rect x="136.284" y="144.835" style="fill:#FFC44F;" width="51.307" height="171.023"></rect> <rect x="221.795" y="102.079" style="fill:#FFC44F;" width="51.307" height="213.779"></rect> <path style="fill:#FFC44F;" d="M460.177,220.856c0.678-3.863,1.051-7.83,1.051-11.887c0-28.8-17.807-53.427-43.003-63.515 l-25.407,63.515L460.177,220.856z"></path> </g> <path style="fill:#FF8C78;" d="M392.818,208.969l25.407-63.515c-7.857-3.146-16.425-4.894-25.407-4.894 c-37.781,0-68.409,30.628-68.409,68.409s30.628,68.409,68.409,68.409c33.725,0,61.73-24.41,67.358-56.522L392.818,208.969z"></path> <path d="M469.779,0H42.221C18.941,0,0,18.941,0,42.221v333.495c0,23.28,18.941,42.221,42.221,42.221h175.646l-3.715,78.029H179.04 c-4.427,0-8.017,3.589-8.017,8.017c0,4.427,3.589,8.017,8.017,8.017H332.96c4.427,0,8.017-3.589,8.017-8.017 c0-4.427-3.589-8.017-8.017-8.017h-35.111l-3.715-78.029h175.645c23.28,0,42.221-18.941,42.221-42.221V144.835 c0-4.427-3.589-8.017-8.017-8.017c-4.427,0-8.017,3.589-8.017,8.017v205.762H179.04c-4.427,0-8.017,3.589-8.017,8.017 c0,4.427,3.589,8.017,8.017,8.017h316.927v9.086c0,14.44-11.748,26.188-26.188,26.188H42.221c-14.44,0-26.188-11.748-26.188-26.188 v-9.086h120.251c4.427,0,8.017-3.589,8.017-8.017c0-4.427-3.589-8.017-8.017-8.017H16.033V67.34h479.933v34.739 c0,4.427,3.589,8.017,8.017,8.017c4.427,0,8.017-3.589,8.017-8.017V42.221C512,18.941,493.059,0,469.779,0z M281.797,495.967 h-51.594l3.715-78.029h44.162L281.797,495.967z M16.033,51.307v-9.086c0-14.44,11.748-26.188,26.188-26.188h427.557 c14.44,0,26.188,11.748,26.188,26.188v9.086H16.033z"></path> <path d="M264.551,392.284c4.427,0,8.017-3.589,8.017-8.017c0-4.427-3.589-8.017-8.017-8.017h-17.102 c-4.427,0-8.017,3.589-8.017,8.017c0,4.427,3.589,8.017,8.017,8.017H264.551z"></path> <path d="M50.772,188.125c-4.427,0-8.017,3.589-8.017,8.017v119.716c0,4.427,3.589,8.017,8.017,8.017h51.307 c4.427,0,8.017-3.589,8.017-8.017V196.142c0-4.427-3.589-8.017-8.017-8.017H50.772z M94.063,307.841H58.789V204.159h35.273V307.841z "></path> <path d="M136.284,136.818c-4.427,0-8.017,3.589-8.017,8.017v171.023c0,4.427,3.589,8.017,8.017,8.017h51.307 c4.427,0,8.017-3.589,8.017-8.017V144.835c0-4.427-3.589-8.017-8.017-8.017H136.284z M179.574,307.841h-35.273v-154.99h35.273 V307.841z"></path> <path d="M221.795,94.063c-4.427,0-8.017,3.589-8.017,8.017v213.779c0,4.427,3.589,8.017,8.017,8.017h51.307 c4.427,0,8.017-3.589,8.017-8.017V102.079c0-4.427-3.589-8.017-8.017-8.017H221.795z M265.086,307.841h-35.273V110.096h35.273 V307.841z"></path> <path d="M421.204,138.011c-9.063-3.629-18.614-5.468-28.386-5.468c-42.141,0-76.426,34.285-76.426,76.426 s34.285,76.426,76.426,76.426c37.189,0,68.837-26.559,75.254-63.151c0-0.001,0.001-0.002,0.001-0.003 c0.777-4.431,1.172-8.896,1.172-13.271c0-31.415-18.851-59.264-48.026-70.951C421.214,138.016,421.209,138.012,421.204,138.011z M392.818,269.361c-33.3,0-60.392-27.092-60.392-60.392s27.092-60.392,60.392-60.392c5.024,0,9.972,0.616,14.789,1.834 l-22.232,55.58c-1.862,4.655,1.124,10.004,6.05,10.872l58.954,10.404C442.557,252.076,419.492,269.361,392.818,269.361z M453.211,208.969c0,0.832-0.018,1.667-0.055,2.508l-49.232-8.688l18.571-46.425C441.384,166.969,453.211,186.85,453.211,208.969z"></path> </g></svg>`
  },
  {
    titulo: "Contenido Actualizado",
    descripcion: "Preguntas actualizadas regularmente por expertos",
    svg: `<svg height="64px" width="64px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 512 512" xml:space="preserve" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path style="fill:#F6F6F7;" d="M503.983,93.528c0-18.891-15.314-34.205-34.205-34.205H42.221c-18.891,0-34.205,15.314-34.205,34.205 v410.455h319.599l176.367-176.367V93.528z"></path> <path style="fill:#D9DCDF;" d="M84.977,59.324H42.221c-18.891,0-34.205,15.314-34.205,34.205v410.455h42.756V93.528 C50.772,74.638,66.086,59.324,84.977,59.324z"></path> <path style="fill:#FF8C78;" d="M503.983,93.528c0-18.891-15.314-34.205-34.205-34.205H42.221c-18.891,0-34.205,15.314-34.205,34.205 v59.858h495.967V93.528z"></path> <path style="fill:#DF7A6E;" d="M84.977,59.324H42.221c-18.891,0-34.205,15.314-34.205,34.205v59.858h42.756V93.528 C50.772,74.638,66.086,59.324,84.977,59.324z"></path> <g> <path style="fill:#B3B9BF;" d="M136.284,110.63L136.284,110.63c-9.446,0-17.102-7.656-17.102-17.102V25.119 c0-9.446,7.656-17.102,17.102-17.102l0,0c9.446,0,17.102,7.656,17.102,17.102v68.409C153.386,102.974,145.73,110.63,136.284,110.63 z"></path> <path style="fill:#B3B9BF;" d="M256,110.63L256,110.63c-9.446,0-17.102-7.656-17.102-17.102V25.119 c0-9.446,7.656-17.102,17.102-17.102l0,0c9.446,0,17.102,7.656,17.102,17.102v68.409C273.102,102.974,265.446,110.63,256,110.63z"></path> <path style="fill:#B3B9BF;" d="M367.165,110.63L367.165,110.63c-9.446,0-17.102-7.656-17.102-17.102V25.119 c0-9.446,7.656-17.102,17.102-17.102l0,0c9.446,0,17.102,7.656,17.102,17.102v68.409 C384.267,102.974,376.611,110.63,367.165,110.63z"></path> </g> <g> <path style="fill:#8E959F;" d="M136.284,93.528V25.119c0-6.327,3.444-11.837,8.551-14.796c-2.517-1.458-5.432-2.307-8.551-2.307 c-9.446,0-17.102,7.656-17.102,17.102v68.409c0,9.446,7.656,17.102,17.102,17.102c3.119,0,6.034-0.849,8.551-2.307 C139.728,105.365,136.284,99.855,136.284,93.528z"></path> <path style="fill:#8E959F;" d="M256,93.528V25.119c0-6.327,3.444-11.837,8.551-14.796c-2.517-1.458-5.432-2.307-8.551-2.307 c-9.446,0-17.102,7.656-17.102,17.102v68.409c0,9.446,7.656,17.102,17.102,17.102c3.119,0,6.034-0.849,8.551-2.307 C259.444,105.365,256,99.855,256,93.528z"></path> <path style="fill:#8E959F;" d="M367.165,93.528V25.119c0-6.327,3.444-11.837,8.551-14.796c-2.517-1.458-5.432-2.307-8.551-2.307 c-9.446,0-17.102,7.656-17.102,17.102v68.409c0,9.446,7.656,17.102,17.102,17.102c3.119,0,6.034-0.849,8.551-2.307 C370.609,105.365,367.165,99.855,367.165,93.528z"></path> </g> <g> <path style="fill:#509FE8;" d="M179.038,461.228c-20.56,0-39.81-8.087-54.203-22.773c-6.197-6.325-6.096-16.476,0.228-22.673 c6.325-6.199,16.476-6.096,22.673,0.228c8.312,8.482,19.428,13.152,31.301,13.152c24.164,0,43.825-19.66,43.825-43.825 c0-24.164-19.66-43.825-43.825-43.825c-8.855,0-16.033-7.179-16.033-16.033s7.179-16.033,16.033-16.033 c24.164,0,43.825-19.66,43.825-43.825s-19.66-43.825-43.825-43.825c-11.538,0-22.431,4.447-30.67,12.521 c-6.326,6.197-16.477,6.094-22.673-0.23c-6.197-6.325-6.095-16.476,0.23-22.673c14.271-13.983,33.134-21.684,53.113-21.684 c41.846,0,75.891,34.045,75.891,75.891c0,24.295-11.472,45.958-29.288,59.858c17.815,13.9,29.288,35.563,29.288,59.858 C254.929,427.182,220.885,461.228,179.038,461.228z"></path> <path style="fill:#509FE8;" d="M391.747,205.762c0-6.124-3.488-11.713-8.989-14.403c-5.501-2.689-12.053-2.013-16.887,1.747 l-76.96,59.858c-6.991,5.436-8.249,15.51-2.812,22.499c5.436,6.991,15.511,8.249,22.499,2.812l51.084-39.732v197.031h32.067 V205.762z"></path> </g> <polygon style="fill:#D9DCDF;" points="327.616,503.983 503.983,327.616 327.616,327.616 "></polygon> <path d="M179.038,421.144c-9.7,0-18.784-3.816-25.575-10.747c-9.282-9.469-24.54-9.623-34.011-0.341 c-9.47,9.283-9.623,24.54-0.341,34.01l0,0c15.913,16.235,37.195,25.178,59.928,25.178c46.267,0,83.908-37.641,83.908-83.908 c0-22.787-9.059-44.126-25.141-59.858c16.083-15.732,25.141-37.071,25.141-59.858c0-46.267-37.641-83.908-83.908-83.908 c-22.091,0-42.946,8.514-58.725,23.974c-9.47,9.281-9.625,24.539-0.344,34.011c9.282,9.47,24.538,9.624,34.01,0.345 c6.733-6.597,15.633-10.23,25.059-10.23c19.745,0,35.808,16.063,35.808,35.808c0,19.745-16.063,35.808-35.808,35.808 c-13.262,0-24.05,10.788-24.05,24.05s10.788,24.05,24.05,24.05c19.745,0,35.808,16.063,35.808,35.808 C214.845,405.081,198.782,421.144,179.038,421.144z M179.038,333.495c-4.421,0-8.017-3.596-8.017-8.017s3.596-8.017,8.017-8.017 c28.585,0,51.841-23.256,51.841-51.841s-23.256-51.841-51.841-51.841c-13.649,0-26.533,5.26-36.28,14.811 c-3.158,3.093-8.243,3.042-11.337-0.114c-3.094-3.158-3.042-8.243,0.114-11.337c12.763-12.506,29.633-19.393,47.503-19.393 c37.426,0,67.875,30.449,67.875,67.875c0,21.032-9.551,40.545-26.202,53.538c-4.048,3.16-4.048,9.482,0,12.641 c16.652,12.992,26.202,32.506,26.202,53.538c0,37.426-30.449,67.875-67.875,67.875c-18.388,0-35.605-7.233-48.478-20.368 c-2.752-2.856-3.046-7.399-0.594-10.544c2.935-3.766,8.706-4.087,12.045-0.68c9.833,10.033,22.981,15.558,37.026,15.558 c28.585,0,51.841-23.256,51.841-51.841C230.879,356.751,207.623,333.495,179.038,333.495z"></path> <path d="M510.183,332.696c1.165-1.416,1.817-3.25,1.817-5.084V93.528c0-23.28-18.941-42.221-42.221-42.221h-77.495V25.119 C392.284,11.268,381.016,0,367.165,0c-13.851,0-25.119,11.268-25.119,25.119v26.188h-60.927V25.119C281.119,11.268,269.851,0,256,0 s-25.119,11.268-25.119,25.119v26.188h-69.478V25.119C161.403,11.268,150.135,0,136.284,0c-13.851,0-25.119,11.268-25.119,25.119 v26.188H42.221C18.941,51.307,0,70.248,0,93.528v153.921c0,4.427,3.589,8.017,8.017,8.017s8.017-3.589,8.017-8.017v-86.046h402.438 c4.427,0,8.017-3.589,8.017-8.017c0-4.427-3.589-8.017-8.017-8.017H16.033V93.528c0-14.44,11.748-26.188,26.188-26.188h68.944 v26.188c0,13.851,11.268,25.119,25.119,25.119c13.851,0,25.119-11.268,25.119-25.119V67.34h69.478v26.188 c0,13.851,11.268,25.119,25.119,25.119c13.851,0,25.119-11.268,25.119-25.119V67.34h60.927v26.188 c0,13.851,11.268,25.119,25.119,25.119c13.851,0,25.119-11.268,25.119-25.119V67.34h77.495c14.44,0,26.188,11.748,26.188,26.188 v51.841h-34.739c-4.427,0-8.017,3.589-8.017,8.017c0,4.427,3.589,8.017,8.017,8.017h34.739v158.196h-96.203V205.762 c0-9.119-5.292-17.598-13.484-21.604c-8.19-4.004-18.133-2.976-25.331,2.621l-76.96,59.858c-10.468,8.142-12.36,23.282-4.219,33.749 c8.044,10.343,23.395,12.273,33.749,4.219l38.146-29.668v64.663h-24.049c-4.427,0-8.017,3.589-8.017,8.017v168.351H16.033V290.205 c0-4.427-3.589-8.017-8.017-8.017S0,285.777,0,290.205v213.779C0,508.411,3.589,512,8.017,512h319.599 c2.101,0,4.184-0.863,5.67-2.349l176.364-176.365C509.838,333.098,510.015,332.902,510.183,332.696z M145.37,93.528 c0,5.01-4.076,9.086-9.086,9.086c-5.01,0-9.086-4.076-9.086-9.086V25.119c0-5.01,4.076-9.086,9.086-9.086 c5.01,0,9.086,4.076,9.086,9.086V93.528z M265.086,93.528c0,5.01-4.076,9.086-9.086,9.086c-5.01,0-9.086-4.076-9.086-9.086V25.119 c0-5.01,4.076-9.086,9.086-9.086c5.01,0,9.086,4.076,9.086,9.086V93.528z M376.251,93.528c0,5.01-4.076,9.086-9.086,9.086 s-9.086-4.076-9.086-9.086V25.119c0-5.01,4.076-9.086,9.086-9.086s9.086,4.076,9.086,9.086V93.528z M303.676,271.948 c-3.446,2.68-8.576,2.045-11.249-1.407c-2.678-3.456-2.045-8.565,1.406-11.25l76.96-59.858c5.164-4.013,12.939-0.2,12.939,6.328 v113.838h-16.033v-81.055c0-6.533-7.77-10.346-12.938-6.328L303.676,271.948z M484.63,335.633L335.633,484.63V335.633H484.63z"></path> </g></svg>`
  }
]);

const testimonioSection = ref([
  {
    nombre: "Ana García",
    cargo: "TAI 2023",
    texto: "Gracias a PreparaTAI conseguí aprobar mi oposición en el primer intento."
  },
  {
    nombre: "Carlos Rodríguez",
    cargo: "Opositor TAI",
    texto: "La mejor plataforma para preparar el examen. Las preguntas son muy similares a las reales."
  }
]);

const tarifas = ref([
  {
    nombre: "Básico",
    precio: "0.99€",
    periodo: "mes",
    caracteristicas: [
      "3 exámenes oficiales",
      "Más de 200 preguntas",
      "Soporte por email",
      "Actualización mensual"
    ],
    boton: "Empezar Gratis",
    destacado: false
  },
  {
    nombre: "Premium",
    precio: "3.99€",
    periodo: "mes",
    caracteristicas: [
      "Todos los exámenes",
      "Más de 3000 preguntas",
      "Estadísticas personales",
      "Soporte prioritario",
      "Preguntas nuevas semanales"
    ],
    boton: "Elegir Plan",
    destacado: true
  },
  {
    nombre: "Avanzado",
    precio: "99.99€",
    periodo: "mes",
    caracteristicas: [
      "Tutor personal",
      "Todo el Premium",
      "Contenido personalizado",
      "Informes detallados"
    ],
    boton: "Contactar",
    destacado: false
  }
]);
</script>

<template>

  <main class="landing">
    <!-- olas animacion -->
    <section class="wave-content portada">
      <div class="portada-imagen1">
        <img src="../img/mujer.png" alt="Imagen de portada">
      </div>
      <div class="portada-contenido">
        <h1 class="portada-titulo">Bienvenido a <span class="subrayado">PreparaTAI</span></h1>
        <p class="portada-descripcion">{{ descripcion }}</p>
        <div class="portada-botones">
          <RouterLink to="/contacto" class="boton btn-primario">Apúntate</RouterLink>
          <RouterLink to="/tai" class="boton btn-secundario">Saber Más</RouterLink>
        </div>
      </div>

      <!-- olas animacion -->
      <div class="wave wave1"></div>
      <div class="wave wave2"></div>
      <div class="wave wave3"></div>
      <div class="wave wave4"></div>
    </section>



    <!-- Características -->
    <section class="elegirSection">
      <h2 class="elegirSection-titulo">¿Por qué elegir PreparaTAI?</h2>
      <div class="elegirSection-contenedor">
        <div v-for="(item, index) in elegirSection" :key="index" class="cardElegir">
          <div class="cardElegir-icono" v-html="item.svg"></div>
          <h3 class="cardElegir-titulo">{{ item.titulo }}</h3>
          <p class="cardElegir-descripcion">{{ item.descripcion }}</p>
        </div>
      </div>
    </section>

    <!-- Mini CTA -->
    <section class="mini-cta">
      <div class="mini-cta-contenido">
        <h2 class="mini-cta-titulo">¿Quieres probar nuestra plataforma?</h2>
        <p class="mini-cta-descripcion">Comienza con una prueba gratuita de 7 días</p>

      </div>
      <div class="mini-cta-botones">
        <RouterLink to="/registro" class="boton boton--miniCta">Prueba Gratis</RouterLink>
      </div>
    </section>

    <!-- TestimonioSection -->
    <section class="testimonioSection">
      <h2 class="testimonioSection-titulo">Lo que dicen nuestros usuarios</h2>
      <div class="testimonioSection-contenedor">
        <div v-for="(testimonioCard, index) in testimonioSection" :key="index" class="testimonioCard">
          <p class="testimonioCard-texto">"{{ testimonioCard.texto }}"</p>
          <div class="testimonioCard-autor">
            <strong>{{ testimonioCard.nombre }}</strong>
            <span>{{ testimonioCard.cargo }}</span>
          </div>
        </div>
      </div>
    </section>



    <!-- Tarifas -->
    <section class="tarifas">
      <h2 class="tarifas-titulo">Nuestros Planes</h2>
      <h4 class="tarifas-subtitulo">Elige el plan que mejor se adapte a tus necesidades</h4>

      <div class="tarifas-contenedor">
        <div v-for="(plan, index) in tarifas" :key="index" class="tarifa-card"
          :class="{ 'tarifa-card-destacado': plan.destacado }">

          <div class="tarifa-card-cabecera">
            <h3 class="tarifa-card-nombre">{{ plan.nombre }}</h3>
            <div class="tarifa-card-precio">
              <span class="precio">{{ plan.precio }}</span>
              <span class="periodo">/ {{ plan.periodo }}</span>
            </div>
          </div>

          <ul class="tarifa-card-caracteristicas">
            <li v-for="(caracteristica, index) in plan.caracteristicas" :key="index" class="caracteristica">
              ✓ {{ caracteristica }}
            </li>
          </ul>
          <!--La clase adicional 'tarifa-card-boton-destacado' se aplica sólo si el plan está marcado como destacado (plan.destacado es true)-->
          <button class="tarifa-card-boton" :class="{ 'tarifa-card-boton-destacado': plan.destacado }">
            {{ plan.boton }}
          </button>
        </div>
      </div>
    </section>


    <!-- CTA Final -->
    <section class="cta">
      <div class="cta-contenido">
        <h2 class="cta-titulo">¿Listo para empezar tu preparación?</h2>
        <p class="cta-descripcion">Únete a miles de opositores que ya confían en nosotros</p>
        <RouterLink to="/contacto" class="boton boton--grande">
          Comienza Tu Preparación
        </RouterLink>
      </div>
    </section>


  </main>
</template>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-weight: unset;
}

.landing {
  background-color: var(--color-fondo);

}

/* Portada  */
.portada {
  justify-content: center;
  display: flex;
  align-items: center;
  color: var(--color-fondo);


}

.portada-imagen1 {

}

.portada-contenido {
  max-width: 800px;

  /*margin: 0 auto;*/
  text-align: center;
}

.portada-titulo {
  font-size: 3.5rem;
  font-weight: lighter;
  padding-bottom: 1.5rem;
  line-height: 1.2;
  color: var(--color-texto-principal);

}

.subrayado {
  font-size: 3.5rem;
  font-weight: 800;
  padding: 0.5rem;
  line-height: 1.2;
  color: var(--color-resaltar);
  background-color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 10px;
  text-decoration-color: var(--color-secundario);

}

.portada-descripcion {
  font-size: 1.5rem;
  padding-bottom: 2rem;
  line-height: 1.6;
  color: var(--color-texto-secundario);


}

.portada-botones {
  display: flex;
  gap: 1rem;
  justify-content: center;
}

/* ¿Por qué elegir PreparaTAI? */
.elegirSection {
  padding-top: 1.5rem;
  padding-bottom: 8rem;
  background-color: var(--color-fondo);
  color: var(--color-texto-principal);
}

.elegirSection-titulo {
  text-align: center;
  font-size: 2.5rem;
  margin-bottom: 3rem;
  color: var(--color-texto-principal);

  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);
}

.elegirSection-contenedor {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
  max-width: 1200px;
  margin: 0 auto;
}

.cardElegir {
  flex: 1;
  min-width: 300px;
  text-align: center;
  padding: 2rem;
  background: var(--color-fondo);
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.cardElegir-icono {
  font-size: 3rem;
  margin-bottom: 1rem;
}

.cardElegir-titulo {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  color: var(--color-primario);
}

/* MINI CTA */
.mini-cta {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 10rem;
  padding: 1.5rem 1rem;
  background: linear-gradient(45deg,var(--color-terciario),var(--color-resaltar2));
  color: var(--color-fondo);

}


.mini-cta-contenido {
  justify-content: center;
}

.mini-cta-titulo {
  font-size: 2rem;
  padding-bottom: 0.5rem;
  color: var(--color-fondo);
}

.mini-cta-descripcion {
  font-size: 1.2rem;
  padding-bottom: 1rem;
  color: var(--color-fondo);
}


.boton--miniCta {
  text-align: center;
  font-size: 1.1rem;
  background-color: var(--color-primario);
  color: var(--color-fondo);
  width: 250px;
}


/* TestimonioSection */
.testimonioSection {
  padding-top: 6rem;
  padding-bottom: 2rem;
  background-color: var(--color-fondo);
  color: var(--color-texto-secundario)
}

.testimonioSection-titulo {
  text-align: center;
  font-size: 2.5rem;
  margin-bottom: 3rem;
  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);

}

.testimonioSection-contenedor {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
  max-width: 1200px;
  margin: 0 auto;
}

.testimonioCard {
  flex: 1;
  min-width: 300px;
  padding: 2rem;
  background: var(--color-fondo2);
  border-radius: 8px;
}

.testimonioCard-texto {
  font-style: italic;
  margin-bottom: 1rem;
  line-height: 1.6;
}

.testimonioCard-autor {
  display: flex;
  flex-direction: column;
  color: #666;
}

/* CTA */
.cta {
  padding: 5rem 2rem;
  background: linear-gradient(45deg, #0eb582, #4030bd);
  color: var(--color-fondo);
  text-align: center;
}

.cta-contenido {
  max-width: 800px;
  margin: 0 auto;
}

.cta-titulo {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.cta-descripcion {
  font-size: 1.25rem;
  margin-bottom: 2rem;
  opacity: 0.9;
}

/* Botones */
.boton {
  display: inline-block;
  padding: 1rem 2rem;
  border-radius: 4px;
  text-decoration: none;
  font-weight: bold;
  transition: all 0.3s ease;
}

.btn-primario {
  background-color: var(--color-primario);
  color: var(--color-fondo);
}

.btn-primario:hover {
  background-color: var(--color-resaltar);
  color: var(--color-fondo);
}

.btn-secundario {

  border: 2px solid var(--color-primario);
  color: var(--color-primario);
}

.btn-secundario:hover {
  border: 2px solid var(--color-primario);
  color: var(--color-primario);
}

.boton--grande {
  padding: 1.25rem 2.5rem;
  font-size: 1.25rem;
  background-color: var(--color-fondo);
  color: var(--color-secundario);
}

.boton:hover {
  background-color: var(--color-resaltar2);
  color: var(--color-primario);
  transform: translateY(-2px);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* PORTADA */
/* Animación olas */
.wave-content {
  position: relative;
  width: 100%;
  height: 75vh;
  background: var(--color-terciario);
  background: linear-gradient(45deg, var(--color-terciario), var(--color-resaltar2));
  overflow: hidden;

}

.wave {
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 100px;
  background: url('/src/img/wave.png') repeat-x;
  background-size: 1000px 100px;
}

.wave.wave1 {
  animation: animate 30s linear infinite;
  z-index: 1000;
  opacity: 1;
  animation-delay: 0s;
  bottom: 0;

}

.wave.wave2 {
  animation: animate2 15s linear infinite;
  z-index: 999;
  opacity: 0.5;
  animation-delay: -5s;
  bottom: 10px;

}

.wave.wave3 {
  animation: animate 30s linear infinite;
  z-index: 998;
  opacity: 0.2;
  animation-delay: -2s;
  bottom: 15px;

}

.wave.wave4 {
  animation: animate2 15s linear infinite;
  z-index: 997;
  opacity: 0.7;
  animation-delay: -5s;
  bottom: 20px;

}

/* Animacion config portada olas */

@keyframes animate {
  0% {
    background-position-x: 0;
  }

  100% {
    background-position-x: 1000px;
  }
}

@keyframes animate2 {
  0% {
    background-position-x: 0;
  }

  100% {
    background-position-x: -1000px;
  }
}

.tarifas {
  padding-top: 4rem;
  padding-bottom: 9rem;
  background-color: var(--color-fondo);
}

.tarifas-titulo {
  text-align: center;
  font-size: 2.5rem;
  margin-bottom: 1rem;
  color: var(--color-primario);
  text-decoration: underline;
  text-underline-offset: 16px;
  text-decoration-color: var(--color-resaltar);
}

.tarifas-subtitulo {
  text-align: center;
  font-size: 1.3rem;
  padding-top: 0.5rem;
  padding-bottom: 3rem;
  color: var(--color-texto-secundario);
}

.tarifas-contenedor {
  display: flex;
  flex-wrap: wrap;
  gap: 2rem;
  justify-content: center;
  max-width: 1200px;
  margin: 0 auto;
}

.tarifa-card {
  flex: 1;
  min-width: 300px;
  padding: 2rem;
  background: var(--color-fondo);
  color: var(--color-primario);
  border-radius: 8px;
  box-shadow: 0 2px 15px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease;
}

.tarifa-card:hover {
  transform: translateY(-5px);
}

.tarifa-card-destacado {
  background: linear-gradient(45deg, var(--color-terciario), var(--color-resaltar2));
  color: var(--color-primario);
  transform: scale(1.05);
  /**Agrandar precio bueno  */
}

.tarifa-card-cabecera {
  text-align: center;
  margin-bottom: 2rem;
  padding-bottom: 1rem;
  border-bottom: 2px solid #eee;
}

.tarifa-card-destacado .tarifa-card-cabecera {
  border-bottom-color: rgba(255, 255, 255, 0.285);
}

.tarifa-card-nombre {
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

.tarifa-card-precio {
  font-size: 2.5rem;
  font-weight: bold !important;

}

/* mes */
.tarifa-card-precio .periodo {
  font-size: 1rem;
  opacity: 0.8;
}

/* lista caracteristicas */
.tarifa-card-caracteristicas {
  list-style: none;

  padding: 0 1rem 2rem 2.5rem;
}

.caracteristica {
  padding: 0.5rem 0;
  color: var(--color-texto-secundario);
}

.tarifa-card-destacado .caracteristica {
  color: var(--color-primario);
}

.tarifa-card-boton {
  width: 100%;
  padding: 1rem;
  border: none;
  border-radius: 4px;
  background: var(--color-primario);
  color: var(--color-fondo);
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s ease;
}

.tarifa-card-boton:hover {
  background: var(--color-resaltar);
  transform: translateY(-2px);
}

.tarifa-card-boton-destacado {
  background: var(--color-resaltar);
  color: var(--color-primario);
}

.tarifa-card-boton-destacado:hover {
  background: var(--color-resaltar2);
  color: var(--color-texto-secundario);
}



/* Responsive */
@media (max-width: 768px) {
  .portada-titulo {
    font-size: 2.5rem;

  }

  .portada-imagen1 {
    display: none;
  }

  .portada-contenido {
    padding: 4rem;
  }

  .portada-contenido>h1 {
    font-size: 2rem;
  }

  .subrayado {
    font-size: 2rem;
  }

  .portada-descripcion {
    font-size: 1.25rem;
  }

  .portada-botones {
    flex-direction: column;
  }

  .cardElegir,
  .testimonioCard {
    min-width: 100%;
    margin: 1rem 0;
  }

  .boton--grande {
    width: 100%;
  }

  .tarifa-card {
    min-width: 100%;
  }

  .tarifa-card-destacado {
    transform: none;
  }

  .mini-cta {
    flex-direction: column;
    gap: 2rem;
    padding: 2rem;
  }

  .cardElegir {
    width: 50%;
  }

  .tarifa-card {
    min-width: 100%;
  }

  .tarifa-card-destacado {
    transform: none;
  }

  .testimonioSection {
    padding: 3rem 1rem; /* Reduce el padding en móviles */
  }

  .testimonioSection-titulo {
    font-size: 2rem; /* Reduce tamaño del título */
    margin-bottom: 2rem;
  }

  .testimonioCard-texto {
    font-size: 0.9rem; /* Reduce tamaño del texto */
  }
}

</style>