-- Eliminar tablas si existen (para recrearlas limpiamente)
DROP TABLE IF EXISTS 
Imagen_SobreNosotros, 
Reserva, 
Transaccion, 
Cliente, 
Administrador, 
Habitacion, 
TipoDeHabitacion, 
Oferta, 
Publicidad, 
Facilidades, 
Home, 
SobreNosotros, 
Imagen, 
Direccion, 
Contacto;

-- Tabla Contacto
CREATE TABLE Contacto (
    IdContacto INT PRIMARY KEY IDENTITY(1,1),
    Telefono1 VARCHAR(15),
    Telefono2 VARCHAR(15),
    ApartadoPostal VARCHAR(255),
    Email VARCHAR(255)
);

-- Tabla Direccion
CREATE TABLE Direccion (
    IdDireccion INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255)
);

-- Tabla Imagen
CREATE TABLE Imagen (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    [Url] VARCHAR(255),
    Eliminado BIT DEFAULT 0
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
    Descripcion VARCHAR(255),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Publicidad
CREATE TABLE Publicidad (
    IdPublicidad INT PRIMARY KEY IDENTITY(1,1),
    EnlacePublicidad VARCHAR(255),
    Activo BIT DEFAULT 1,
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla SobreNosotros
CREATE TABLE SobreNosotros (
    IdSobreNosotros INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(255)
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
    Nombre VARCHAR(20),
    Descripcion VARCHAR(255),
    TarifaDiaria DECIMAL(10,2),
    IdImagen INT FOREIGN KEY REFERENCES Imagen(IdImagen)
);

-- Tabla Habitacion
CREATE TABLE Habitacion (
    IdHabitacion INT PRIMARY KEY IDENTITY(1,1),
    Numero INT,
    Estado VARCHAR(10),
    IdTipoDeHabitacion INT FOREIGN KEY REFERENCES TipoDeHabitacion(IdTipoDeHabitacion)
);

-- Tabla Administrador
CREATE TABLE Administrador (
    IdAdmin INT PRIMARY KEY IDENTITY(1,1),
    NombreDeUsuario VARCHAR(255),
    Contrasennia VARCHAR(20)
);

-- Tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(20),
    Apellidos VARCHAR(50),
    Email VARCHAR(255),
    TarjetaDePago VARCHAR(30)
);

-- Tabla Transaccion
CREATE TABLE Transaccion (
    IdTransaccion INT PRIMARY KEY IDENTITY(1000,1),
    Fecha DATE,
    Monto DECIMAL(10,2),
    Descripcion VARCHAR(255)
);

-- Tabla Reserva (IdReserva como UNIQUEIDENTIFIER)
CREATE TABLE Reserva (
    IdReserva UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    FechaLlegada DATE,
    FechaSalida DATE,
    Estado VARCHAR(10),
    Activo BIT DEFAULT 1,
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
    IdTipoDeHabitacion INT FOREIGN KEY REFERENCES TipoDeHabitacion(IdTipoDeHabitacion),
    Activo BIT DEFAULT 1
);
