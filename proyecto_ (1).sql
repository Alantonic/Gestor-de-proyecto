create database proyecto /*Crea la base de datos*/


USE proyecto;/*Selecciona la base de datos que se va a utilizar*/
GO


/*Hubo error al usar palabras que identificaba usuario, esto es una solución*/
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'usuario')
BEGIN
    CREATE TABLE usuario(
        ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        Nombre VARCHAR(32) NOT NULL,
        Telefono VARCHAR(20) NOT NULL,
        Gmail VARCHAR(100) not null,

    );
    PRINT 'Tabla creada';

     CREATE TABLE usuarios_login (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    usuario_sesion VARCHAR(50) NOT NULL,
    contraseña VARCHAR(255) NOT NULL
);
    

 CREATE TABLE equipo(
        ID_equipo INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
        Nombre VARCHAR(32) NOT NULL,
        descripciòn VARCHAR(20) NOT NULL,
    );

    Create table equipo_usuario(
        ID_usuarioEquipo Int primary key identity (1,1) not null,
        ID int not null,
        ID_equipo int not null
    );
    PRINT 'Tabla creada';

    create table Proyecto(
        id_proyecto int primary key identity(1,1) not null,
        ID int not null,
        Nombre varchar (30) not null,
        descripcion varchar (500) not null,
        Fecha_Inicio Date not null,
        Fecha_fin date not null,
        Estado varchar(50) not null,
        Presupuesto decimal (10,2) not null

        

    );

    create table Tarea(
        id_tarea int primary key identity (1,1) not null,
        id_proyecto int not null,
        ID int not null,
        titulo varchar (50) not null,
        descripcion varchar (500) not null,
        prioridad varchar (50) not null,
        estado varchar (50) not null,
        fehca_Inicio Date not null,
        fechaVencimiento date not null,
        horaEstimadas int not null
    );

    create table Documento(
        id_documento int primary key identity (1,1) not null,
        id_proyecto int not null,
        nombre varchar (50) not null,
        tipoArchivo varchar (50) not null,
        url varchar (255) not null,
        fechaSubida datetime not null
    );

    create table Clientes(
    Id_Cliente int primary key not null,
    Nombre varchar (50) not null,
    Apellido varchar (50) not null,
    Telefono VARCHAR(20) NOT NULL,
     Gmail VARCHAR(100) not null,
    );
        
    


END
ELSE
BEGIN
    PRINT 'La tabla ya existe, mostrando datos:';
    SELECT * FROM usuario;
    insert into usuario (Nombre, Telefono, gmail) values

('Alan', '66423113214', 'alan@gmail.com')

-- Insertar un proyecto simple
INSERT INTO Proyecto (ID, Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto)
VALUES (1, 'Sistema de Ventas', 'Desarrollo de sistema de ventas para tienda minorista', '2024-01-15', '2024-06-30', 'En progreso', 15000.00);

-- Insertar otro proyecto
INSERT INTO Proyecto (ID, Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto)
VALUES (1, 'App Móvil', 'Aplicación móvil para delivery de comida', '2024-02-01', '2024-08-15', 'Planificación', 25000.00);

-- Insertar proyecto con nombre NULL (porque tu tabla lo permite)
INSERT INTO Proyecto (ID, Nombre, descripcion, Fecha_Inicio, Fecha_fin, Estado, Presupuesto)
VALUES (2, NULL, 'Mantenimiento de servidores', '2024-03-10', '2024-04-10', 'Completado', 5000.00);

INSERT INTO equipo (Nombre, descripciòn) VALUES
('Alpha', 'Equipo de desarrollo'),
('Beta', 'Equipo de diseño'),
('Gamma', 'Equipo de ventas'),
('Delta', 'Equipo de soporte'),
('Omega', 'Equipo de marketing');

INSERT INTO equipo_usuario (ID, ID_equipo) VALUES
-- Usuario 1 en varios equipos
(1, 1),  -- Usuario 1 en equipo Alpha
(1, 2),  -- Usuario 1 en equipo Beta
-- Usuario 2 en equipos
(2, 1),  -- Usuario 2 en equipo Alpha
(2, 3),  -- Usuario 2 en equipo Gamma
-- Usuario 3 en equipos
(3, 2),  -- Usuario 3 en equipo Beta
(3, 4),  -- Usuario 3 en equipo Delta
-- Usuario 4 en equipos
(4, 5),  -- Usuario 4 en equipo Omega
(4, 1),  -- Usuario 4 en equipo Alpha
-- Usuario 5 en equipos
(5, 3),  -- Usuario 5 en equipo Gamma
(5, 4);  -- Usuario 5 en equipo Delta


INSERT INTO Tarea (id_proyecto, ID, titulo, descripcion, prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas)
VALUES (1, 101, 'Diseñar Base de Datos', 'Crear el modelo ER y normalizar tablas', 'Alta', 'Pendiente', '2024-01-10', '2024-01-20', 8);

INSERT INTO Tarea (id_proyecto, ID, titulo, descripcion, prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas)
VALUES (1, 102, 'Desarrollar API', 'Crear endpoints REST para el backend', 'Alta', 'En Progreso', '2024-01-15', '2024-01-25', 16);

INSERT INTO Tarea (id_proyecto, ID, titulo, descripcion, prioridad, estado, fehca_Inicio, fechaVencimiento, horaEstimadas)
VALUES (2, 201, 'Diseñar UI', 'Crear prototipos en Figma', 'Media', 'Completada', '2024-01-05', '2024-01-18', 12);

INSERT INTO Documento (id_proyecto, nombre, tipoArchivo, url, fechaSubida)
VALUES 
(1, 'Requisitos', 'pdf', '/documentos/proyecto1/requisitos.pdf', '2025-01-15 10:30:00'),
(1, 'Diagrama DB', 'png', '/documentos/proyecto1/diagrama.png', '2025-01-16 14:20:00'),
(2, 'Contrato', 'pdf', '/documentos/proyecto2/contrato.pdf', '2025-02-01 09:00:00'),
(2, 'Presupuesto', 'xlsx', '/documentos/proyecto2/presupuesto.xlsx', '2025-02-03 11:45:00'),
(3, 'Wireframes', 'fig', '/documentos/proyecto3/wireframes.fig', '2025-02-10 16:00:00'),
(3, 'Informe Tecnico', 'pdf', '/documentos/proyecto3/informe.pdf', '2025-02-12 08:15:00'),
(4, 'Backup DB', 'sql', '/documentos/proyecto4/backup.sql', '2025-03-01 23:59:00'),
(5, 'Manual Tecnico', 'pdf', '/documentos/proyecto5/manual_tecnico.pdf', '2025-03-05 13:30:00');

INSERT INTO Clientes (Id_Cliente, Nombre, Apellido, Telefono, Gmail)
VALUES 
(1, 'Juan', 'Pérez', '5551234567', 'juan.perez@gmail.com'),
(2, 'María', 'García', '5552345678', 'maria.garcia@hotmail.com'),
(3, 'Carlos', 'López', '5553456789', 'carlos.lopez@yahoo.com'),
(4, 'Ana', 'Martínez', '5554567890', 'ana.martinez@gmail.com'),
(5, 'Luis', 'Rodríguez', '5555678901', 'luis.rodriguez@outlook.com');


/*se ocupa actualizar el atributo Nombre, faltó asginar Not null primero*/
UPDATE Proyecto 
SET Nombre = 'Sin nombre' 
WHERE Nombre IS NULL;

/*se ocupa actualizar el atributo Proyecto, faltó asginar Not null primero*/
UPDATE Proyecto 
SET Presupuesto = 0 
WHERE Presupuesto IS NULL;

/*Modifica la tabla Nombre*/
ALTER TABLE Proyecto
ALTER COLUMN Nombre varchar(30) NOT NULL;


/*Modifica la tabla Telefono*/
ALTER TABLE usuario
ALTER COLUMN Telefono VARCHAR(50) NULL;

/*Modifica la tabla proyecto en la columna presupuesto*/
ALTER TABLE Proyecto
ALTER COLUMN Presupuesto decimal(10,2) NOT NULL;

    alter table usuario 
    add usuario_sesion varchar(32) null,
    contraseña varchar(32) null;
    select * from usuarios_login

alter table Proyecto
alter column ID int NULL;


select * from proyecto

END