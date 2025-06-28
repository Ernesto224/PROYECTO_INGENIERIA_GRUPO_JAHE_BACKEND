-- Contacto y Dirección
INSERT INTO Contacto (Telefono1, Telefono2, ApartadoPostal, Email) 
VALUES ('+506 2222-2222', '+506 8888-8888', 'Apartado 123, San José', 'info@hotel.com');

INSERT INTO Direccion (Descripcion) 
VALUES ('200 metros norte del Parque Central, San José, Costa Rica');

-- Imágenes principales
INSERT INTO Imagen (Ruta) VALUES
('https://images.unsplash.com/photo-1542314831-068cd1dbfeeb'),  -- ID 1: Home
('https://images.unsplash.com/photo-1551632811-561732d1e306'),  -- ID 2: Facilidades
('https://images.pexels.com/photos/2373201/pexels-photo-2373201.jpeg'), -- ID 3: Publicidad
('https://images.pexels.com/photos/258154/pexels-photo-258154.jpeg'), -- ID 4: SobreNosotros
('https://images.unsplash.com/photo-1571896349842-33c89424de2d'), -- ID 5: Oferta
('https://images.unsplash.com/photo-1582719508461-905c673771fd'), -- ID 6: Standard
('https://images.unsplash.com/photo-1578683010236-d716f9a3f461'), -- ID 7: Suite
('https://images.unsplash.com/photo-1611892440504-42a792e24d32'), -- ID 8: Familiar
('https://images.pexels.com/photos/164595/pexels-photo-164595.jpeg'); -- ID 9: Deluxe

-- Contenido web
INSERT INTO Home (Descripcion, IdImagen) 
VALUES ('Bienvenido al mejor hotel de Costa Rica', 1);

-- 1. Piscina de lujo
INSERT INTO Facilidades (Descripcion, IdImagen) 
VALUES ('Piscina infinity climatizada con bordes de cristal que ofrecen una vista espectacular al volcán, equipada con cómodos loungers de lujo, servicio de bebidas y un área de jacuzzi integrado para máximo relax al atardecer', 2);

-- 2. Spa premium
INSERT INTO Facilidades (Descripcion, IdImagen) 
VALUES ('Spa "Serenidad Tropical" con 8 cabinas de tratamiento que incluyen masajes con piedras volcánicas, envolturas de chocolate orgánico, terapia de flotación y rituales de belleza con productos locales. Dispone de circuito termal con sauna finlandesa, baño turco y piscina de hidroterapia', 4);

-- 3. Restaurante gourmet
INSERT INTO Facilidades (Descripcion, IdImagen) 
VALUES ('Restaurante "Sabores del Pacífico" con cocina de autor dirigida por chef internacional, menú degustación de 7 tiempos, cocina abierta donde se preparan platillos con ingredientes locales orgánicos, y una cava de vinos con más de 200 etiquetas seleccionadas de todo el mundo', 5);

-- 4. Suite presidencial
INSERT INTO Facilidades (Descripcion, IdImagen) 
VALUES ('Suite Presidencial "Horizonte Infinito" de 120m² con terraza privada, piscina individual climatizada, sala de estar con chimenea, dormitorio master con vestidor y baño de mármol con jacuzzi para dos, equipada con sistema de domótica y atención de mayordomo 24/7', 7);

-- 5. Centro de negocios
INSERT INTO Facilidades (Descripcion, IdImagen) 
VALUES ('Centro Ejecutivo "Productividad Total" con salas de reuniones equipadas con tecnología 4K, videoconferencia holográfica, servicio de traducción simultánea, impresión 3D, barra de café premium y área de networking con diseño ergonómico para trabajo colaborativo', 10);

INSERT INTO Publicidad (Enlace, IdImagen) 
VALUES ('https://promo.hotel.com', 3);

INSERT INTO SobreNosotros (Descripcion) 
VALUES ('Hotel familiar con 50 años de tradición');

INSERT INTO Imagen_SobreNosotros (IdImagen, IdSobreNosotros) 
VALUES (4, 1);

-- Tipos de Habitación (4 tipos)
INSERT INTO TipoDeHabitacion (Nombre, Descripcion, TarifaDiaria, IdImagen) VALUES
('Standard', 'Habitación básica con cama doble', 75.00, 6),
('Suite', 'Suite con sala independiente y jacuzzi', 150.00, 7),
('Familiar', 'Amplia habitación con 2 camas dobles', 120.00, 8),
('Deluxe', 'Habitación premium con terraza privada', 180.00, 9);

-- Habitaciones (4 por tipo)
INSERT INTO Habitacion (Numero, Estado, FechaEstado, IdTipoDeHabitacion) VALUES
-- Standard
(701, 'DISPONIBLE', '2025-05-11', 1),
(702, 'DISPONIBLE','2025-05-11', 1),
(703, 'DISPONIBLE','2025-05-11', 1),
(704, 'DISPONIBLE','2025-05-11', 1),
-- Suite
(801, 'DISPONIBLE','2025-05-11', 2),
(802, 'DISPONIBLE','2025-05-11', 2),
(803, 'DISPONIBLE','2025-05-11', 2),
(804, 'DISPONIBLE','2025-05-11', 2),
-- Familiar
(901, 'DISPONIBLE','2025-05-11', 3),
(902, 'DISPONIBLE','2025-05-11', 3),
(903, 'DISPONIBLE','2025-05-11', 3),
(904, 'DISPONIBLE','2025-05-11', 3),
-- Deluxe
(1001, 'DISPONIBLE','2025-05-11', 4),
(1002, 'DISPONIBLE','2025-05-11', 4),
(1003, 'DISPONIBLE','2025-05-11', 4),
(1004, 'DISPONIBLE','2025-05-11', 4);

-- Ofertas
INSERT INTO Oferta (Nombre, FechaInicio, FechaFinal, Porcentaje, IdTipoDeHabitacion, IdImagen) 
VALUES ('Otoño 2023', '2023-09-01', '2023-11-30', 15, 1, 5);

-- Temporadas
INSERT INTO Temporada (Nombre, FechaInicio, FechaFinal, Porcentaje) 
VALUES ('Alta Temporada', '2023-12-15', '2024-01-15', 20);