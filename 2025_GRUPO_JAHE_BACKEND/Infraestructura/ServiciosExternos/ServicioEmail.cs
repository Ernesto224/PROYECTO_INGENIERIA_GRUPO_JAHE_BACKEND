using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Infraestructura.ServiciosExternos
{
    public class ServicioEmail : IServicioEmail
    {
        private readonly string _emailServiceUrl;
        private readonly HttpClient _httpClient;


        public ServicioEmail(string emailServiceUrl, HttpClient httpClient)
        {
            _emailServiceUrl = emailServiceUrl;
            _httpClient = httpClient;
        }
        public async Task<bool> enviarEmail(string email, string asunto, string mensaje)
        {
            try
            {
                var requestBody = new
                {
                    email = email,
                    subject = asunto,
                    message = mensaje
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_emailServiceUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al enviar email: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }
    }
}
