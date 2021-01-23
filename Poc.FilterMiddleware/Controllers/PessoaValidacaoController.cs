using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poc.FilterMiddleware.Api.Configurations.Filters;
using Poc.FilterMiddleware.Domain.Contract;
using Poc.FilterMiddleware.Domain.ContractValidations;
using Poc.FilterMiddleware.Domain.Features;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Poc.FilterMiddleware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaValidacaoController : ControllerBase
    {

        /// <summary>Feature de processamento de pessoa</summary>
        public IFeaturePessoa FeaturePessoa { get; }

        /// <summary>Construtor da classe</summary>
        /// <param name="featurePessoa">Feature de processamento de pessoa</param>
        public PessoaValidacaoController(IFeaturePessoa featurePessoa)
        {
            FeaturePessoa = featurePessoa;
        }

        [TypeFilter(typeof(ModelStateValidationFilterAttribute), Arguments = new object[] { typeof(PessoaFisicaVMValidator) })]
        [HttpPost]
        public IActionResult Post([FromBody] PessoaFisicaVM pessoa)
        {
            FeaturePessoa.CadastraPessoaFisica(pessoa);

            return Ok();
        }
    }
}
