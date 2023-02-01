using DRD.Core.Messages;
using FluentValidation;

namespace DRD.Catalogo.API.Application.Commands.Produtos
{
    public class CadastrarProdutoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public string NomeCategoria { get; private set; }

        public CadastrarProdutoCommand(string nome, string descricao, decimal valor, string imagem, int quantidadeEstoque, string nomeCategoria, DateTime dataCadastro)
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
            QuantidadeEstoque = quantidadeEstoque;
            NomeCategoria = nomeCategoria;
            DataCadastro = dataCadastro;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CadastrarProdutoValidation : AbstractValidator<CadastrarProdutoCommand>
        {
            public CadastrarProdutoValidation()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto está inválido");

                RuleFor(x => x.Descricao)
                    .NotEmpty()
                    .WithMessage("A descrição do produto está inválido")
                    .MinimumLength(10)
                    .WithMessage("A descrição deve conter no minimo 10 caracteres")
                    .MaximumLength(250)
                    .WithMessage("A descrição excedeu o limite de 250 caracteres");

                RuleFor(x => x.Imagem)
                    .NotEmpty()
                    .WithMessage("A Url da imagem do produto é obrigatória");

                RuleFor(x => x.NomeCategoria)
                    .NotEmpty()
                    .WithMessage("O nome da categoria é inválido");
            }
        }

    }
}
