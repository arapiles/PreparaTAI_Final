# PreparaTAI Backend

Backend de la aplicación web PreparaTAI. Desarrollado en .NET

## ⚙️ Prerrequisitos

- .NET 7.0 SDK o superior
- SQL Server 2019 o superior
- Visual Studio 2022 (recomendado) o VS Code

## 🛠️ Tecnologías

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- LINQ
- CORS habilitado para seguridad


## 📚 Configuración de Base de Datos SQL Server

Preparar SQL Server

Instalar SQL Server (versión 2019 o superior)
Asegurarse que SQL Server Browser esté en ejecución
Habilitar autenticación SQL Server y Windows


## 💾 Restaurar Base de Datos desde Backup

Por seguridad para no subir la base de datos a github. En el aula virtual encontrarás en la copia de seguridad del codigo dentro del rar un archivo `.bak` que contiene una copia de seguridad de la base de datos.

### Pasos para Restaurar

1. **Abrir SQL Server Management Studio**
   - Conectarse al servidor SQL Server local

2. **Restaurar Base de Datos**
   - Click derecho en "Databases"
   - Seleccionar "Restore Database..."
   - En "Source", seleccionar "Device"
   - Click en "..." para buscar el archivo
   - Navegar hasta la ubicación de `APITAI.bak`
   - Seleccionar el archivo y click en "OK"

3. **Configurar Restauración**
   - Verificar que el nombre de la base de datos sea "APITAI"
   - En la pestaña "Files", comprobar las rutas de los archivos
   - Click en "OK" para iniciar la restauración

4. **Verificar Restauración**
   - Refrescar el nodo "Databases"
   - La base de datos "apitai" debería aparecer
   - Expandir la base de datos para ver las tablas
   - Comprobar que existan:
     - USUARIOS
     - PREGUNTAS
     - RESULTADOS

## ⚠️ Solución de Problemas Comunes

Error de Conexión: Verificar que el servicio de SQL Server esté en ejecución
No se muestra las tablas: Verificar cadena de conexión

## Instalación

> [!CAUTION]
> Antes de empezar asegúrate de tener todos los prerequisitos de arriba

Ejecutar la solución de .NET `WebApiProyecto.sln` con Visual Studio 2022 para preparar automáticamente todo el proyecto del backend. Automáticamente detecta las dependencias y restaura las versiones especificadas en el proyecto.

Lo único que debes hacer es:

- Abrir el archivo .sln con Visual Studio
- Esperar a que termine la restauración automática de paquetes
- Ejecutar el proyecto

