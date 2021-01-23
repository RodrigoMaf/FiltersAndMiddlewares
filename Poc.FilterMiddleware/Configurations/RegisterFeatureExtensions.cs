using Microsoft.Extensions.DependencyInjection;
using Poc.FilterMiddleware.Application.Features;
using Poc.FilterMiddleware.Domain.Features;

namespace Poc.FilterMiddleware.Api.Configurations
{
    /// <summary>Classe para registro de features do projeto</summary>
    public static class RegisterFeatureExtensions
    {
        /// <summary>Registra as features do projeto</summary>
        /// <param name="services">Serviço de injeção de dependencia</param>
        public static IServiceCollection RegisterFeatures(this IServiceCollection services)
            => services.AddSingleton<IFeaturePessoa, FeaturePessoa>();

    }
}
