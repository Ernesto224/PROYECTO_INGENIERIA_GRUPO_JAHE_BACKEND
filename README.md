# PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND
# Sistema de Reservación de Hoteles

## Descripción
Este es un sistema de reservación de hoteles que permite a los usuarios buscar, reservar y administrar sus estadías en hoteles registrados. El sistema está dividido en dos repositorios independientes:

- **Frontend:** Desarrollado en [Tecnología utilizada, por ejemplo, Angular o React], proporcionando una interfaz intuitiva para los usuarios.
- **Backend:** Construido en [Tecnología utilizada, por ejemplo, Node.js con Express o Spring Boot], encargado de la lógica del negocio y la gestión de datos.

El proyecto también se relaciona con un **drive** que almacena archivos relevantes, como documentación, diseños de bases de datos y otros recursos.

## Tecnologías Utilizadas

### Frontend
- [Framework o librería utilizada]
- Tailwind CSS / Bootstrap
- Consumo de API REST
- Manejo de estado con [Redux, Context API, etc.]

### Backend
- [Lenguaje y framework utilizado]
- Base de datos: [MySQL, PostgreSQL, MongoDB, etc.]
- Autenticación con JWT / OAuth
- Arquitectura basada en microservicios / MVC

## Instalación y Configuración
### Clonar los repositorios
```bash
# Clonar el frontend
git clone https://github.com/Ernesto224/PROYECTO_INGENIERIA_GRUPO_JAHE_FRONTEND.git

# Clonar el backend
git clone https://github.com/Ernesto224/PROYECTO_INGENIERIA_GRUPO_JAHE_BACKEND.git
```

## Estructura del Proyecto
Este proyecto sigue la arquitectura Domain-Driven Design (DDD) con principios SOLID, dividiendo la aplicación en capas bien definidas:

- API (Capa de presentación)
- Aplicación (Capa de aplicación)
- Dominio (Capa de dominio)
- Infraestructura (Capa de infraestructura)

A continuación, se explica la función de cada capa y cómo se maneja una consulta de datos (Read) en esta arquitectura.
```
/
├── API                            # Capa de presentación (Web API)
│   ├── Controllers                # Controladores que manejan las solicitudes HTTP
│   ├── Properties                 # Configuraciones del proyecto
│   ├── appsettings.json           # Archivo de configuración (conexión a BD, logs, etc.)
│   ├── Program.cs                 # Punto de entrada del backend
│
├── Aplicacion                     # Capa de aplicación (casos de uso y lógica de aplicación)
│   ├── Agregados                  # Agregados DDD que encapsulan entidades relacionadas
│   ├── Comandos                   # Operaciones que modifican el estado de la aplicación
│   ├── Consultas                  # Operaciones de solo lectura sobre los datos
│   ├── DTOs                       # Objetos de transferencia de datos (Data Transfer Objects)
│   ├── Handlers                   # Manejadores de comandos y consultas (CQRS)
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

