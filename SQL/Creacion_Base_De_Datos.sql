USE DB_Pruebas_C08380;
-- Eliminar tablas si existen (para recrearlas limpiamente)
DROP TABLE IF EXISTS
Imagen_SobreNosotros, 
Reserva, 
Transaccion, 
Cliente, 
Usuario, 
Habitacion, 
TipoDeHabitacion,
Imagen,
Oferta, 
Publicidad, 
Facilidades, 
Home, 
SobreNosotros,  
Direccion, 
Contacto,
Temporada;

-- Tabla Contacto
CREATE TABLE Contacto (
    IdContacto INT PRIMARY KEY IDENTITY(1,1),
    Telefono1 VARCHAR(15),
    Telefono2 VARCHAR(15),
    ApartadoPostal NVARCHAR(255),
    Email NVARCHAR(MAX)
);

-- Tabla Direccion
CREATE TABLE Direccion (
    IdDireccion INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(MAX)
);

-- Tabla Imagen
CREATE TABLE Imagen (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    Ruta NVARCHAR(MAX),
    Activa BIT DEFAULT 1
);

-- Tabla Home
CREATE TABLE Home (
    IdHome INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(MAX),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Facilidades
CREATE TABLE Facilidades (
    IdFacilidad INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(MAX),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Publicidad
CREATE TABLE Publicidad (
    IdPublicidad INT PRIMARY KEY IDENTITY(1,1),
    Enlace NVARCHAR(MAX),
    Activa BIT DEFAULT 1,
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla SobreNosotros
CREATE TABLE SobreNosotros (
    IdSobreNosotros INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(MAX)
);

-- Tabla Imagen_SobreNosotros (relación entre Imagen y SobreNosotros)
CREATE TABLE Imagen_SobreNosotros (
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen),
    IdSobreNosotros INT FOREIGN KEY REFERENCES SobreNosotros(IdSobreNosotros),
    PRIMARY KEY (IdImagen, IdSobreNosotros)
);

-- Tabla TipoDeHabitacion
CREATE TABLE TipoDeHabitacion (
    IdTipoDeHabitacion INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100),
    Descripcion NVARCHAR(MAX),
    TarifaDiaria DECIMAL(10,2),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Habitacion
CREATE TABLE Habitacion (
    IdHabitacion INT PRIMARY KEY IDENTITY(1,1),
    Numero INT,
    Estado VARCHAR(10),
    Activa BIT DEFAULT 1,
    FechaEstado DATE,
    IdTipoDeHabitacion INT FOREIGN KEY REFERENCES TipoDeHabitacion(IdTipoDeHabitacion)
);

-- Tabla Usuario (anteriormente IdAdmin cambiado a IdUsuario)
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(100),
    Contrasennia NVARCHAR(MAX),
    Rol VARCHAR(100)
);

-- Tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(20),
    Apellidos VARCHAR(50),
    Email NVARCHAR(MAX),
    TarjetaDePago VARCHAR(30)
);

-- Tabla Transaccion
CREATE TABLE Transaccion (
    IdTransaccion INT PRIMARY KEY IDENTITY(1000,1),
    Fecha DATE,
    Monto DECIMAL(10,2),
    Descripcion NVARCHAR(MAX)
);

-- Tabla Reserva (IdReserva como UNIQUEIDENTIFIER)
CREATE TABLE Reserva (
    IdReserva UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    FechaLlegada DATE,
    FechaSalida DATE,
    Estado VARCHAR(10),
    Activa BIT DEFAULT 1,
    IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente),
    IdHabitacion INT FOREIGN KEY REFERENCES Habitacion(IdHabitacion),
    IdTransaccion INT FOREIGN KEY REFERENCES Transaccion(IdTransaccion)
);

-- Tabla Oferta
CREATE TABLE Oferta (
    IdOferta INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255),
    FechaInicio DATE,
    FechaFinal DATE,
    Porcentaje INT,
    Activa BIT DEFAULT 1,
    IdTipoDeHabitacion INT FOREIGN KEY REFERENCES TipoDeHabitacion(IdTipoDeHabitacion),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Temporada (sin claves foráneas, se aplica de forma general)
CREATE TABLE Temporada (
    IdTemporada INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(255),
    FechaInicio DATE,
    FechaFinal DATE,
    Porcentaje INT
);