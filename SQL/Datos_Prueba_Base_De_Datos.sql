-- Insertar datos en la tabla Imagen
INSERT INTO Imagen ([Url], Eliminado) 
VALUES 
('https://res.cloudinary.com/dks1ifnxa/image/upload/v1717887282/samples/landscapes/beach-boat.jpg', 0);

-- Insertar datos en la tabla Home usando los IdImagen insertados
INSERT INTO Home (Descripcion, IdImagen) 
VALUES 
('Bienvenido a Jade – Tu refugio de confort y elegancia Descubre una experiencia única en [Ubicación del Hotel], donde el lujo, la comodidad y el servicio excepcional se combinan para brindarte una estancia inolvidable. Ya sea que viajes por negocios o placer, nuestras habitaciones cuidadosamente diseñadas, gastronomía de primer nivel y amenidades exclusivas te harán sentir como en casa.Relájate en nuestra piscina, disfruta de un masaje en el spa o explora los encantos de la ciudad con nuestras recomendaciones personalizadas. ¡Reserva ahora y vive la hospitalidad en su máxima expresión!', 1);
