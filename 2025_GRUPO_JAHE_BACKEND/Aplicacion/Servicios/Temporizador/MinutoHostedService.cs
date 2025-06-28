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
    public class MinutoHostedService : IHostedService, IDisposable
    {
        private bool _isRunning = false;

        private Timer? _timer;

        private readonly IServiceProvider _serviceProvider;

        public MinutoHostedService(
            IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
            callback: DoWork,
            state: null,
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            if (_isRunning)
            {
                return;
            }

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var repositorio = scope.ServiceProvider
                        .GetRequiredService<IHabitacionRepositorio>();

                    await Task.Run(() => repositorio.ActualizarHabitacionesOcupadasConTimeout());
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                _isRunning = false;
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
