using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Categorias
{
    public class DesabilitarCategoriaCommand : Command
    {
        public string Nome { get; private set; }

        public DesabilitarCategoriaCommand(string nome)
        {
            Nome = nome;
        }

        public override bool EhValido()
        {
            ValidationResult = new DesabilitarCategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class DesabilitarCategoriaValidation : AbstractValidator<DesabilitarCategoriaCommand>
        {
            public DesabilitarCategoriaValidation()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .WithMessage("O nome da categoria é obrigatório");
            }
        }
    }
}
