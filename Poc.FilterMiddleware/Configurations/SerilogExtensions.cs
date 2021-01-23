using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Poc.FilterMiddleware.Api.Configurations
{
    /// <summary>Classe para configuração do serilog no serviço</summary>
    public static class SerilogExtensions
    {        
        /// <summary>Configuração do serilog via arquivo json no serviço</summary>
        /// <param name="loggerFactory">Provedor de log do serviço</param>
        /// <param name="configuration">Provedor de configuração do serviço</param>
        public static void UseSerilog(this ILoggerFactory loggerFactory, IConfiguration configuration) 
        {
            var logConfig = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration);

            loggerFactory.AddSerilog(logConfig.CreateLogger());
        }     
    }
}
