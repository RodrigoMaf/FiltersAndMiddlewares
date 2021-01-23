using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Poc.FilterMiddleware.Domain.ContractValidations;

namespace Poc.FilterMiddleware.Api.Configurations
{
    /// <summary>Classe para registro de features do projeto</summary>
    public static class RegisterValidationExtensions
    {
        /// <summary>Registra as features do projeto</summary>
        /// <param name="services">Serviço de injeção de dependencia</param>
        public static IServiceCollection RegisterValidations(this IServiceCollection services)
            => services
                .AddSingleton<PessoaFisicaVMValidator>()
                .AddScoped<Func<Type, IValidator>>(s => t => (IValidator)s.GetRequiredService(t));

    }
}
