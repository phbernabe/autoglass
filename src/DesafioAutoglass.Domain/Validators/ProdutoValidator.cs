using DesafioAutoglass.Domain.Models;
using FluentValidation;

namespace DesafioAutoglass.Domain.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotNull().WithMessage("Descrição obrigatória.")
                .Length(1, 200).WithMessage("Descrição deve ter no máximo de 200 caracteres.");

            RuleFor(x => x.DataFabricacao)
                .LessThan(x => x.DataValidade)
                .WithMessage("Data de Validade não pode ser anterior a Data de Fabricação.");

            RuleFor(x => x.Situacao).NotNull();
        }
    }
}
