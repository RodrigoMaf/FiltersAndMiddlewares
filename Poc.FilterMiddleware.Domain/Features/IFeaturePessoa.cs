using Poc.FilterMiddleware.Domain.Contract;

namespace Poc.FilterMiddleware.Domain.Features
{
    /// <summary>Interdace com implementação de dados de pessoa </summary>
    public interface IFeaturePessoa
    {
        /// <summary>Processa do cadastro de uma pessoa</summary>
        void CadastraPessoaFisica(PessoaFisicaVM pessoa);
    }
}
