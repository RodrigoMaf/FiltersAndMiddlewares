using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Poc.FilterMiddleware.Api.Configurations.Filters
{
    /// <summary>Classe de filtro para validação de modelo de dados</summary>
    public class ModelStateValidationFilterAttribute  : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else 
            {
                await next();
            }
        }
    }
}
