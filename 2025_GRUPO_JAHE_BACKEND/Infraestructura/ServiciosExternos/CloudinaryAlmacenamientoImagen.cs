using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dominio.Interfaces;

namespace Infraestructura.ServiciosExternos
{
    public class CloudinaryAlmacenamientoImagen : IServicioAlmacenamientoImagenes
    {
        private readonly string _cloudinaryUrl;

        public CloudinaryAlmacenamientoImagen(string cloudinaryUrl)
        {
            _cloudinaryUrl = cloudinaryUrl;
        }

        public async Task<string> SubirImagen(byte[] imagen, string nombreArchivo)
        {
            try
            {
                // Se crea una conexion a Cloudinary mediante un objeto de la libreria CloudinaryDodNet
                Cloudinary cloudinary = new Cloudinary(this._cloudinaryUrl);

                // Se obtiene el nombre del archivo
                var fileName = nombreArchivo;

                // Se almacena la imagen en un MemoryStream
                using var stream = new MemoryStream(imagen);

                // Se crea un objeto de tipo ImageUploadParams con los parametros necesarios para subir la imagen
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, stream),
                    UploadPreset = "Subir_a_Galeria_Hotel_Jade"
                };

                // Se sube la imagen a Cloudinary
                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                // Verificar si la carga fue exitosa
                if (uploadResult.Error != null)
                {
                    Console.WriteLine($"Error al subir la imagen ANTES DEL CATCH: {uploadResult.Error.Message}");
                    throw new Exception($"Error al subir la imagen: {uploadResult.Error.Message}");
                }

                // Se retorna la URL de la imagen subida
                return uploadResult.SecureUrl?.ToString() ?? throw new Exception("No se obtuvo una URL válida tras la carga.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al subir la imagen a Cloudinary: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
