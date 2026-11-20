# 🌎 LenguaViva — Cliente de escritorio

Aplicación de escritorio para administrar **LenguaViva**, una escuela de idiomas. Permite al personal gestionar alumnos, profesores, cursos, horarios, inscripciones, pagos y reportes. Se conecta por WCF al [servidor de LenguaViva](https://github.com/AxelLuna04/LenguaVivaServer).

<!-- Agrega aquí capturas de pantalla, por ejemplo:
![Menú principal](docs/menu.png)
-->

## ✨ Funcionalidades

- **Inicio de sesión** para administradores y profesores.
- **Usuarios:** registrar, buscar, ver detalles y editar administradores, profesores y alumnos.
- **Cursos:** registrar idiomas y cursos, buscarlos, editarlos y asignarles horario.
- **Alumnos:** inscribirlos a cursos, registrar pagos, generar el **recibo en PDF** y consultar su historial de cursos.
- **Reportes:** alumnos por curso, ingresos, profesores activos y horario de cada profesor.

## 🧰 Tecnologías

- C# con **WPF** (.NET Framework 4.8)
- **WCF** (`netTcpBinding`) para comunicarse con el servidor
- Patrón de navegación por páginas con ventanas reutilizables (confirmación, mensajes emergentes)

## 🗂️ Estructura

```
LenguaVivaCliente/
├── Vistas/
│   ├── InicioSesion/   # Login
│   ├── Menu/           # Menú principal y submenús
│   ├── Registros/      # Alta de administradores, profesores y alumnos
│   ├── Ediciones/      # Edición de usuarios
│   ├── Alumnos/        # Inscripciones, pagos, recibos e historial
│   ├── Cursos/         # Cursos, idiomas, horarios y reportes de curso
│   └── Reportes/       # Reportes de profesores
├── VentanasReutilizables/  # Diálogos comunes
├── Utilidades/
└── Connected Services/     # Referencia al servicio WCF
```

## ⚙️ Cómo correrlo

1. Levanta primero el [servidor](https://github.com/AxelLuna04/LenguaVivaServer) (escucha en `net.tcp://localhost:8000`).
2. Abre `LenguaVivaCliente.sln` en Visual Studio 2022.
3. Restaura los paquetes NuGet y ejecuta el proyecto.

<!--
## 👤 Mi participación
Este fue un proyecto en equipo. Describe aquí qué partes desarrollaste tú.
-->
