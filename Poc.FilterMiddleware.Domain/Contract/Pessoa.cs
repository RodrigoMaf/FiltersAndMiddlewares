using System;

namespace Poc.FilterMiddleware.Domain.Contract
{
    /// <summary>Dados de exemplo de pessoa</summary>
    public class PessoaFisica
    {
        /// <summary>Nome da pessoa</summary>
        public string Nome { get; set; }

        /// <summary>Data de nascimento</summary>
        public DateTime Nascimento { get; set; }

        /// <summary>Documento CPF</summary>
        public string Cpf { get; set; }

        /// <summary>Genero da pessoa</summary>
        public Genero? Genero { get; set; }
    }

    public enum Genero 
    {        
        Masculino = 1,
        Feminino = 2,
        Outro = 3
    }
}
