# Sistema de Reservación de Hoteles 2025_GRUPO_JAHE_BACKEND API (.NET 8)

## 🧾 Descripción
Esta es la solución backend del sistema para el Hotel. Expone una API RESTful desarrollada con .NET 8, siguiendo los principios SOLID y arquitectura DDD, que sirve tanto al cliente como al administrador del hotel.

## 🏗️ Arquitectura
Se implementa la arquitectura **Domain-Driven Design (DDD) con principios SOLID**, estructurada en capas:
```
/
├── API                            # Capa de presentación (Web API)
│   ├── Controllers                # Controladores que manejan las solicitudes HTTP
│   ├── appsettings.json           # Archivo de configuración (conexión a BD, logs, etc.)
│   ├── Program.cs                 # Punto de entrada del backend
│
├── Aplicacion                     # Capa de aplicación (casos de uso y lógica de aplicación)
│   ├── Constantes                  # Mensajes de validación, nombres de claims
│   ├── DTOs                        # Objetos de transferencia de datos (Data Transfer Objects)
│   ├── Interfaces                  # Interfaces de servicios de aplicación
│   ├── Servicios                   # Implementaciones de servicios de aplicación
│
├── Dominio                        # Capa de dominio (reglas de negocio y modelos)
│   ├── Entidades                   # Entidades de negocio con identidad propia
│   ├── Enumeraciones               # Definición de enums usados en el dominio
│   ├── Excepciones                 # Excepciones específicas de la lógica de dominio
│   ├── Interfaces                  # Contratos de repositorios y otros servicios del dominio
│   ├── ObjetosDeValor              # Value Objects usados en el dominio
│
├── Infraestructura                 # Capa de infraestructura (acceso a datos, configuración, integración)
│   ├── Configuraciones             # Configuraciones de Entity Framework y mapeo de entidades
│   ├── Persistencia                # Implementación del DbContext de Entity Framework
│   ├── Repositorios                # Implementaciones de repositorios para acceso a datos
│   ├── ServiciosExternos           # Integraciones con APIs y servicios externos
```

## 🚀 Tecnologías y Herramientas
- .NET 8
- C#
- Entity Framework Core
- SQL Server
- LINQ
- Cloudinary (para almacenamiento de imágenes)

## 📦 Paquetes NuGet utilizados
- `CloudinaryDotNet`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Swashbuckle.AspNetCore` (si se usa Swagger)

## ⚙️ Configuración
1. Configurar la cadena de conexión en `appsettings.json`.

## ▶️ Ejecución
```
cd 2025_GRUPO_JAHE_BACKEND/API dotnet run
```

## 📁 Estructura del Repositorio
```
/
├── 2025_GRUPO_JAHE_BACKEND/
├── SQL/              # Scripts de creación de base de datos
├── .gitignore
└── README.md
```

## 📎 Enlaces Relacionados
- Repositorio Cliente Angular - PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND
```
https://github.com/Ernesto224/PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND.git
```
- Repositorio Administrador Angular - PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND_ADMIN
```
https://github.com/Ernesto224/PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND_ADMIN.git
```
