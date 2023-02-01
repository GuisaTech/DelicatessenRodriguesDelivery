using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Categorias
{
    public class CadastrarCategoriaCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string ImagemUrl { get; private set; }

        public CadastrarCategoriaCommand(string nome, string imagemUrl)
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Nome = nome;
            ImagemUrl = imagemUrl;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarCategoriaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CadastrarCategoriaValidation : AbstractValidator<CadastrarCategoriaCommand>
        {
            public CadastrarCategoriaValidation()
            {
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
