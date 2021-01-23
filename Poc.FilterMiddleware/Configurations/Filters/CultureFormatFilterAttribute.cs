using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Poc.FilterMiddleware.Api.Configurations.Filters
{
    /// <summary>Filtro pra setar a cultura da api</summary>
    public class CultureFormatFilterAttribute : ActionFilterAttribute
    {
        #region Properties

        /// <summary>Dados de configuração de resources</summary>
        private RequestLocalizationOptions LocalizeSettings { get; }

        #endregion

        /// <summary>Inicia uma nova instância da classe <see cref="CultureFormatFilterAttribute" />.</summary>
        /// <param name="localizeOptions">Dados de configuração de resources</param>
        public CultureFormatFilterAttribute(
                                              IOptions<RequestLocalizationOptions> localizeOptions
                                           )
        {
            LocalizeSettings = localizeOptions.Value;
        }

        /// <summary>Evento usado para setar a cultura da api</summary>
        /// <param name="context">Contexto Http</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetCulture(context);
            base.OnActionExecuting(context);
        }

        /// <summary>Evento usado para setar a cultura da api</summary>
        /// <param name="context">Contexto Http</param>
        /// <param name="next">Próxima execução do pipeline</param>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            SetCulture(context);
            return base.OnActionExecutionAsync(context, next);
        }

        /// <summary>Seta a cultura de acordo com a informada no header. Caso não existente usará a default</summary>
        /// <param name="context">Contexto Http</param>
        private void SetCulture(ActionExecutingContext context) 
        {
           var supportedCultures = LocalizeSettings
                                      .SupportedUICultures
                                      .Select(o => o.Name);

            StringValues requestCulture = LocalizeSettings
                                            .DefaultRequestCulture
                                            .Culture
                                            .Name;
            context
                .HttpContext
                .Request
                .Headers
                .TryGetValue("Accept-Language", out requestCulture);

            CultureInfo
                .CurrentUICulture = supportedCultures
                                        .Contains(requestCulture.ToString()) ?
                                            new CultureInfo(requestCulture) :
                                            LocalizeSettings.DefaultRequestCulture.UICulture;
        }
    }
}
