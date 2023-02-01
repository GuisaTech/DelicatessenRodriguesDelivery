using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Categorias
{
    public class AlterarDisponibilidadeCategoriaCommand : Command
    {
        public string Nome { get; private set; }

        public AlterarDisponibilidadeCategoriaCommand(string nome)
        {
            Nome = nome;
        }

        public override bool EhValido()
        {
            ValidationResult = new AlterarDisponibilidadeValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AlterarDisponibilidadeValidation : AbstractValidator<AlterarDisponibilidadeCategoriaCommand>
        {
            public AlterarDisponibilidadeValidation()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .WithMessage("O nome da categoria é obrigatório");
            }
        }
    }
}
