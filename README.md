# REAL FOOTBALL 3D — Paquete preparado para compilación en la nube

## Qué incluye
- Estructura de proyecto Unity con `ProjectSettings/`, `Packages/` y `Assets/`.
- Scripts del prototipo y archivo de plantillas 2026/27.
- Un generador de escena inicial: al abrir el proyecto en Unity Editor crea una cancha sencilla, balón, dos jugadores de prueba, cámara y gestores.
- La escena se registra en Build Settings automáticamente.

## Importante: estado real
Este paquete **no es un APK**. Es una base importable para Unity y preparada para conectarse a un servicio de compilación en la nube. La escena generada usa cápsulas y primitivas como placeholders; no contiene modelos 3D realistas, animaciones completas, IA de 22 jugadores ni los modos torneo/carrera terminados. Los nombres y dorsales del JSON son una instantánea de referencia que debe verificarse antes de publicar.

## Requisitos
- Cuenta Unity.
- Repositorio Git (GitHub recomendado).
- Servicio Unity Build Automation compatible con tu plan/cuenta.
- Android build target configurado en el servicio.

## Pasos desde Android
1. Descarga y descomprime este ZIP con un gestor de archivos.
2. En Chrome abre https://github.com/ y crea una cuenta o inicia sesión.
3. Crea un repositorio nuevo llamado `real-football-3d` y súbele el contenido de la carpeta `REAL_FOOTBALL_3D_Prototype` (no subas el ZIP como único archivo: Unity debe ver `Assets`, `Packages` y `ProjectSettings` en la raíz).
4. Abre https://dashboard.unity.com/ e inicia sesión con la misma cuenta Unity.
5. Busca **DevOps / Build Automation** (el nombre o disponibilidad puede variar por cuenta y región), conecta el repositorio y selecciona la rama principal.
6. Configura un target Android. Usa Unity 2022.3 LTS. Activa la compilación APK si el panel permite elegir entre APK y AAB.
7. Inicia una compilación. Si el servicio solicita licencia Unity, sigue el flujo de activación que ofrece Unity.
8. Cuando el build termine correctamente, descarga el APK desde el panel y permite la instalación desde esa fuente en Android.

## Si aparece un error
- `No Android module`: selecciona Android como plataforma y habilita Android Build Support en la configuración del servicio.
- Error de licencia: vincula/activa una licencia Unity válida en Build Automation.
- Error de escena: abre el proyecto en Unity Editor una vez; el script `Assets/Editor/RF3DProjectBootstrap.cs` crea `Assets/Scenes/Main.unity` y la registra.
- Error de compilación C#: comparte aquí el texto o una captura del error para corregirlo.

## Aviso de tamaño y calidad
El primer APK, si la compilación funciona, será una prueba técnica básica. No representa todavía el juego final tipo EA SPORTS FC. Se necesitará desarrollo adicional para jugabilidad, IA, animaciones, audio, optimización y licencias de marcas/activos.
