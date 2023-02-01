using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Produtos
{
    public class DesabilitarProdutoCommand : Command
    {
        public string Nome { get; private set; }

        public DesabilitarProdutoCommand(string nome)
        {
            Nome = nome;
        }

        public override bool EhValido()
        {
            ValidationResult = new DesabilitarProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DesabilitarProdutoValidation : AbstractValidator<DesabilitarProdutoCommand>
        {
            public DesabilitarProdutoValidation()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto é obrigatório");
            }
        }
    }
}
