import { createRouter, createWebHistory } from "vue-router";
import PaginaInicio from "@/views/InicioView.vue";
import PaginaListarPreguntas from "@/views/dashboard/AdminPreguntaView.vue";
import PaginaInicioSesion from "@/views/LoginView.vue";
import PaginaExamen from "@/views/ExamenView.vue";
import PaginaTai from "@/views/TaiView.vue";
import PaginaNosotros from "@/views/NosotrosView.vue";
import PaginaContacto from "@/views/ContactoView.vue";
import PaginaAdminUsuarios from "@/views/dashboard/AdminUsuariosView.vue";
import PaginaUsuarioDatos from "@/views/UsuarioDatosView.vue";
import PaginaDashboardAdmin from "@/views/dashboard/DashboardView.vue";

// IMPORTANTE: Proteger rutas con beforeEnter!!!!!!
const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            // Página pública
            path: '/',
            name: 'Inicio',
            component: PaginaInicio
        },
        {
            path: '/examen',
            name: 'Examen',
            component: PaginaExamen,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    // Verificar tipo de usuario
                    // Convertimos string en localstorage a un objeto js para poder acceder a sus propiedades o sale undefined
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 2) { // Usuario normal
                        next() // Si es usuario normal puede acceder a examen
                    } else {
                        next('/administracion') // Si es admin redirige a administración
                    }
                } else {
                    next('/login') // Si no está logueado redirige a login
                }
            }
        },
        {   
            path: '/resultados',
            name: 'Resultados',
            component: PaginaUsuarioDatos,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    // Verificar tipo de usuario
                    // Convertimos string en localstorage a un objeto js para poder acceder a sus propiedades o sale undefined
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 2) { // Usuario normal
                        next() // Si es usuario normal puede acceder a examen
                    } else {
                        next('/administracion') // Si es admin redirige a administración
                    }
                } else {
                    next('/login') // Si no está logueado redirige a login
                }
            }
        },
        {
            // Página pública
            path: '/tai',
            name: 'Tai',
            component: PaginaTai
        },
        {
            // Página pública
            path: '/nosotros',
            name: 'Nosotros',
            component: PaginaNosotros
        },
        {
            // Página pública
            path: '/contacto',
            name: 'Contacto',
            component: PaginaContacto
        },
        {
            path: '/admin_preguntas',
            name: 'Preguntas',
            component: PaginaListarPreguntas,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 1) { // Administrador
                        next()
                    } else {
                        next('/examen') // Si es usuario normal, redirigir a examen
                    }
                } else {
                    next('/login')
                }
            }
        },
        {
            path: '/control_panel',
            name: 'Dashboard',
            component: PaginaDashboardAdmin,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 1) { // Administrador
                        next()
                    } else {
                        next('/examen') // Si es usuario normal, redirigir a examen
                    }
                } else {
                    next('/login')
                }
            }
        },
        {
            path: '/admin_usuarios',
            name: 'Usuarios',
            component: PaginaAdminUsuarios,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 1) { // Administrador
                        next()
                    } else {
                        next('/examen')
                    }
                } else {
                    next('/login')
                }
            }
        },
        {
            // Página pública para hacer el login
            path: '/login',
            name: 'Login',
            component: PaginaInicioSesion,
            beforeEnter: (to, from, next) => {
                const datosUsuario = localStorage.getItem('datosUsuario')
                if (datosUsuario !== null) {
                    // Si ya está logueado, redirigir según su tipo
                    const usuario = JSON.parse(datosUsuario)
                    if (usuario.TipoUsuario === 1) {
                        next('/admin_preguntas')
                    } else {
                        next('/examen')
                    }
                } else {
                    next()
                }
            }
        },
        {
            path: '/:pathMatch(.*)*',
            redirect: () => ({ name: 'Inicio' })
        }
    ]
});

export default router;