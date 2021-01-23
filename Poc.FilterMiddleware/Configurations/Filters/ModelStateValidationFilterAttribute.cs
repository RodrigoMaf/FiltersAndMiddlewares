using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Poc.FilterMiddleware.Api.Configurations.Filters
{
    /// <summary>Classe de filtro para validação de modelo de dados</summary>
    public class ModelStateValidationFilterAttribute  : ActionFilterAttribute
    {
        /// <summary>Validação customizada da classe</summary>
        private IValidator Validator { get; }

        /// <summary>Logger do sistema para o filtro</summary>
        private ILogger<ModelStateValidationFilterAttribute> Logger { get; }

        public ModelStateValidationFilterAttribute(ILogger<ModelStateValidationFilterAttribute> logger, Func<Type, IValidator> validationFactory, Type type = null)
        {
            Logger = logger;
            if (type != null && type.GetInterface(nameof(IValidator)) != null)
            {
                Validator = validationFactory(type);
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            BadRequestObjectResult validateResult = null;
            var argObjects = context.ActionArguments.Where(o => o.Value.GetType().Namespace.Contains("Poc.FilterMiddleware"));

            if (Validator != null)
            {
                foreach (var arg in argObjects)
                {
                    var validationResult = Validator.Validate(arg.Value);
                    if (!validationResult.IsValid)
                    {
                        Logger.LogWarning($"Houve erros na validação com o resultado => {string.Join(",", validationResult.Errors.Select(o => o.ErrorMessage))}");
                        validateResult = new BadRequestObjectResult(validationResult.Errors.Select(o => new { o.PropertyName, o.ErrorMessage }));
                    }
                }
            }

            if (validateResult != null)
            {
                context.Result = validateResult;
            }
            else 
            {
                await next();
            }
        }
    }
}
