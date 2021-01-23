using System;
using Microsoft.AspNetCore.Mvc;
using Poc.FilterMiddleware.Domain.Contract;
using Poc.FilterMiddleware.Domain.Features;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Poc.FilterMiddleware.Api.Controllers.Ruim
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaValidacaoController : ControllerBase
    {

        /// <summary>Feature de processamento de pessoa</summary>
        public IFeaturePessoa FeaturePessoa { get; set; }

        /// <summary>Construtor da classe</summary>
        /// <param name="featurePessoa">Feature de processamento de pessoa</param>
        public PessoaValidacaoController(IFeaturePessoa featurePessoa)
        {
            FeaturePessoa = featurePessoa;
        }

        // POST api/<PessoaValidacaoController>
        [HttpPost]
        public IActionResult Post([FromBody] PessoaFisicaVM pessoa)
        {

            if (string.IsNullOrWhiteSpace(pessoa.Nome)) 
            {
                return BadRequest("O Nome é obrigatório");
            }

            if (pessoa.Nascimento.Date > DateTime.Now.AddYears(-18).Date)
            {
                return BadRequest("A pessoa tem que ter mais de 18 anos.");
            }

            if (string.IsNullOrWhiteSpace(pessoa.Cpf))
            {
                return BadRequest("O documento é obrigatório");
            }

            if (pessoa.Genero.HasValue == false)
            {
                return BadRequest("Genero é obrigatório");
            }

            FeaturePessoa.CadastraPessoaFisica(pessoa);

            return Ok();
        }
    }
}
