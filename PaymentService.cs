﻿using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Payment.Host
{
    public class PaymentService : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IBusControl busControl, ILogger<PaymentService> logger)
        {
            _busControl = busControl;
            _logger = logger;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("正在启动服务总线");

            try
            {
                await _busControl.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "启动服务总线时发生错误");
                throw;
            }

            _logger.LogInformation("支付 微服务已启动");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync();

            _logger.LogInformation("支付 微服务已停止");
        }
    }
}
