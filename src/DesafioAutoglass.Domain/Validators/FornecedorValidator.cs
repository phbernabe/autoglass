using DesafioAutoglass.Domain.Models;
using FluentValidation;

namespace DesafioAutoglass.Domain.Validators
{
    public class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(x => x.Descricao)
                .MaximumLength(200).WithMessage("Descrição deve ter no máximo de 200 caracteres..");

            RuleFor(x => x.Cnpj)
                .MaximumLength(14).WithMessage("CNPJ inválido.");
        }
    }
}
