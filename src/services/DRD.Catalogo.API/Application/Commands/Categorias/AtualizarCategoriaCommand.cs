using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Categorias
{
    public class AtualizarCategoriaCommand : Command
    {
        public string NomeAntigo { get; private set; }
        public string Nome { get; private set; }
        public string ImagemUrl { get; private set; }

        public AtualizarCategoriaCommand(string nomeAntigo, string nome, string imagemUrl)
        {
            NomeAntigo = nomeAntigo;
            Nome = nome;
            ImagemUrl = imagemUrl;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarCategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarCategoriaValidation : AbstractValidator<AtualizarCategoriaCommand>
        {
            public AtualizarCategoriaValidation()
            {
                RuleFor(x => x.NomeAntigo)
                    .NotEmpty()
                    .WithMessage("O nome antigo da categoria não foi informada");

                RuleFor(x => x.Nome)
                    .MinimumLength(3)
                    .WithMessage("A categoria deve ter no minimo 3 caracteres")
                    .NotEmpty()
                    .WithMessage("O nome da categoria não foi informada");

                RuleFor(x => x.ImagemUrl)
                    .NotEmpty()
                    .WithMessage("A categoria precisa conter a Url de uma imagem")
                    .MaximumLength(1000)
                    .WithMessage("A url da imagem deve conter no maximo 1000 (mil) caracteres");
            }
        }
    }
}
