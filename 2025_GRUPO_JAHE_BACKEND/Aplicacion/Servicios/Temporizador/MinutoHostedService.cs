using Dominio.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.Temporizador
{
    public class MinutoHostedService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;

        public MinutoHostedService(
            IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    try
                    {
                        var repositorio = scope.ServiceProvider
                            .GetRequiredService<IHabitacionRepositorio>();

                        repositorio.ActualizarHabitacionesOcupadasConTimeout();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }


    }
}
