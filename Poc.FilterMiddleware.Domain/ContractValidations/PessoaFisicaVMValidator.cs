using System;
using FluentValidation;
using Poc.FilterMiddleware.Domain.Contract;

namespace Poc.FilterMiddleware.Domain.ContractValidations
{
    public class PessoaFisicaVMValidator : AbstractValidator<PessoaFisicaVM>
    {
        public PessoaFisicaVMValidator()
        {
            RuleFor(o => o.Nome)
                .Must(o => string.IsNullOrWhiteSpace(o) == false)
                .WithMessage("O Nome é obrigatório");

            RuleFor(o => o.Nascimento)
                .Must(o => o.Date <= DateTime.Now.AddYears(-18).Date)
                .WithMessage("A pessoa tem que ter mais de 18 anos.");

            RuleFor(o => o.Cpf)
                .Must(o => string.IsNullOrWhiteSpace(o) == false)
                .WithMessage("O documento é obrigatório");

            RuleFor(o => o.Nome)
                .Must(o => string.IsNullOrWhiteSpace(o) == false)
                .WithMessage("Genero é obrigatório");

        }
    }
}
