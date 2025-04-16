-- Insertar datos en la tabla Imagen
INSERT INTO Imagen ([Url], Eliminado) 
VALUES 
('https://res.cloudinary.com/dks1ifnxa/image/upload/v1743058256/1642576565_jusr_right_uvhlyf.jpg', 0);

-- Insertar datos en la tabla Home usando los IdImagen insertados
INSERT INTO Home (Descripcion, IdImagen) 
VALUES 
('Bienvenido a Jade – Tu refugio de confort y elegancia Descubre una experiencia única en [Ubicación del Hotel], donde el lujo, la comodidad y el servicio excepcional se combinan para brindarte una estancia inolvidable. Ya sea que viajes por negocios o placer, nuestras habitaciones cuidadosamente diseñadas, gastronomía de primer nivel y amenidades exclusivas te harán sentir como en casa.Relájate en nuestra piscina, disfruta de un masaje en el spa o explora los encantos de la ciudad con nuestras recomendaciones personalizadas. ¡Reserva ahora y vive la hospitalidad en su máxima expresión!', 1);


INSERT INTO Facilidades (Descripcion, IdImagen) VALUES('Puede relajarse en nuestra piscina al aire libre, ideal para refrescarse en un ambiente tranquilo. Deléitese con una exquisita selección de platillos locales e internacionales en nuestro restaurante, acompañado de bebidas refrescantes en el bar', 3)

INSERT INTO Facilidades (Descripcion, IdImagen) VALUES('le ofrecemos Wi-Fi gratuito de alta velocidad en todas las áreas del hotel para que siempre esté conectado. Si desea mantener su rutina de ejercicios, nuestro gimnasio totalmente equipado está disponible para su comodidad', 1)

INSERT INTO Habitacion (Numero, Estado, IdTipoDeHabitacion)
VALUES 
(1, 'Disponible', 1),
(2, 'En reserva', 1),
(3, 'Reservado', 1),
(4, 'No disp', 1),
(5, 'Disponible', 2),
(6, 'En reserva', 2),
(7, 'Reservado', 2),
(8, 'No disp', 2),
(9, 'Disponible', 3),
(10, 'En reserva', 3),
(11, 'Reservado', 3),
(12, 'No disp', 3),
(13, 'Disponible', 4),
(14, 'En reserva', 4),
(15, 'Reservado', 4),
(16, 'No disp', 4)