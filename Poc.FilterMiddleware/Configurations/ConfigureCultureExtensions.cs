using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Poc.FilterMiddleware.Api.Configurations
{
    /// <summary>Classe de configurações de cultura do serviço</summary>
    public static class ConfigureCultureExtensions
    {
        /// <summary>Registra uma configuração de culturas permitidas no sistema</summary>
        /// <param name="services">Provedor de configuração de DI para o serviço</param>
        /// <param name="configuration">Provedor de dados de configurações do serviço</param>
        public static IServiceCollection AddCultureService(
                                                              this IServiceCollection services,
                                                              IConfiguration configuration
                                                          )
            => services
                .AddLocalization()
                .Configure<RequestLocalizationOptions>(options =>
                {
                    options.SupportedCultures.Clear();
                    options.SupportedUICultures.Clear();
                    options.RequestCultureProviders.Clear();

                    configuration
                        .GetSection("RequestLocalizationOptions")
                        .Bind(options);

                    var supportedCultures = options
                                               .SupportedCultures
                                               .Select(o => new StringSegment(o.Name))
                                               .ToList();

                    var supportedUiCultures = options
                                               .SupportedUICultures
                                               .Select(o => new StringSegment(o.Name))
                                               .ToList();

                    options
                        .DefaultRequestCulture = new RequestCulture(
                                                                      options.SupportedCultures.First(),
                                                                      options.SupportedUICultures.First()
                                                                   );
                    options
                        .RequestCultureProviders
                        .Insert(
                            0,
                            new CustomRequestCultureProvider(context =>
                            {
                                return Task.FromResult(new ProviderCultureResult(supportedCultures, supportedUiCultures));
                            })
                        );
                });

        /// <summary>Configura o uso de cultura</summary>
        /// <param name="app">Provedor de builder da aplicação</param>
        /// <param name="localization">Dados de configuração de culturas</param>
        public static IApplicationBuilder UseCulture(
                                                        this IApplicationBuilder app,
                                                        IOptionsMonitor<RequestLocalizationOptions> localization
                                                    )
            => app
                .UseRequestLocalization(localization.CurrentValue);
    }
}
