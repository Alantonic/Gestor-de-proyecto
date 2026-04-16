# Gestor-de-proyectos
Aplicación de escritorio desarrollada en C# bajo el entorno de Visual Studio 2022, diseñada para la administración centralizada de proyectos. El sistema permite el registro, almacenamiento y visualización de datos críticos como nombres de proyectos, gestión de participantes y descripciones detalladas. Utiliza SQL Server como motor de base de datos relacional, garantizando la persistencia y la integridad de la información mediante operaciones CRUD (Crear, Leer, Actualizar, Eliminar).

# Herramientas

  - C#: Lenguaje de programación oriendado a objetos desarrollado por Microsoft, fue dieñado para
    la plataforma .NET, permitiendo la creación de aplicaciones de escritorio.
  - SQL Server Management Studio 21: Es un entorno integrado para poder administrar infraestructuras
    de SQL (Structure Query Lenguage).

# Interfaces
- Check-in: Area de registro, por el momento el usuario puede registrarse sin rellenar datos, caso contrario el programa registra los datos y lo envía a la base de datos
- Login: despues de rellenar los datos dentro de Check-in, login llama a la base de datos para
  consultar los datos guardados de usuario y contraseña
- Main: Es una interfaz que muestra secciones donde el usuario pueda ver datos en diferentes       ventanas de una forma intuitiva y organizada, cabe recalcar que cada sección se debe             interactuar con "doble click".
- Proyecto: Como proposito de este gestor de proyectos es guardar datos de un proyecto que esté
  en desarrollo o finalizado, lo muestra en la sección de proyectos en una tabla, el usuario       puede agregar o eliminar mas datos guardados.
- Usuarios: Mostrará una tabla de los usuarios que trabajan.
- Documentos: Guardará reportes en una tabla.
- Tareas: guardará pendientes extras.

# Arquitectura
El proyecto está desarrollado utilizando arquitectura en capas, separando responsabilidades en diferentes módulos:

- Interfaces: Contiene las vistas del sistema (Windows Forms)
- Controles: Manejan la lógica entre la interfaz y los datos
- Modelos: Representación de entidades del sistema
- Repositorio: Implementación del patrón Repository para acceso a datos
- Conexion_Base_Datos: Gestión de conexión a SQL Server

Esta estructura permite un código más organizado, mantenible y escalable.

# instrucciones
1- Para abrir este archivo se integro dentro de la carpeta principal el archivo .Sln donde pueden interactuar con todo el proyecto.
2- En el app.config está la cadena de conexión de la base de datos para cambiarlo
3- Es necesario cumplir con rellenar los campo de registro.
4- Elegir una sección con doble click.
5- Dar click derecho en tablas con información para ver opciones como editar o eliminar.

#Paquetes instalados en administrador de paquetes NuGet
- EntityFramework
- System.Configuration.ConfigurationManager
