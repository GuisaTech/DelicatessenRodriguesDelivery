using DRD.Catalogo.API.Models;
using DRD.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace DRD.Catalogo.API.Application.Commands.Produtos
{
    public class ProdutoCommandHandler : CommandHandler,
        IRequestHandler<CadastrarProdutoCommand, ValidationResult>,
        IRequestHandler<AtualizarProdutoCommand, ValidationResult>,
        IRequestHandler<AlterarDisponibilidadeProdutoCommand, ValidationResult>,
        IRequestHandler<DesabilitarProdutoCommand, ValidationResult>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoCommandHandler(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ValidationResult> Handle(CadastrarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var categoria = await _categoriaRepository.ObterPorNome(message.NomeCategoria);

            if(categoria == null)
            {
                AdicionarErro("A categoria selecionada é inválida");
                return ValidationResult;
            }

            var produto = new Produto(message.Nome, message.Descricao,
                message.Valor, DateTime.Now, message.Imagem,
                message.QuantidadeEstoque, categoria.Id);

            if (await _produtoRepository.ExisteNoBanco(message.Nome))
            {
                AdicionarErro("Nome do produto já existe");
                return ValidationResult;
            }

            _produtoRepository.Adicionar(produto);

            return await PersistirDados(_produtoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            if (await _produtoRepository.ExisteNoBanco(message.Nome))
            {
                AdicionarErro("Nome do produto já existe");
                return ValidationResult;
            }

            var categoria = await _categoriaRepository.ObterPorNome(message.NomeCategoria);

            if (categoria == null)
            {
                AdicionarErro("A categoria selecionada é inválida");
                return ValidationResult;
            }

            var produto = await _produtoRepository.ObterPorNome(message.NomeAntigo);

            if (produto == null)
            {
                AdicionarErro("Houve um erro ao atualizar o produto");
                return ValidationResult;
            }

            produto.AtualizarValor(message.Valor);
            produto.AtualizarCategoria(categoria.Id);
            produto.AtualizarDescricao(message.Descricao);
            produto.AtualizarImagem(message.Imagem);
            produto.AtualizarNome(message.Nome);
            produto.AtualizarQauntidadeEstoque(message.QuantidadeEstoque);

            _produtoRepository.Atualizar(produto);

            return await PersistirDados(_produtoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AlterarDisponibilidadeProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            await _produtoRepository.MudarStatusDisponibilidade(message.Nome);

            return await PersistirDados(_produtoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DesabilitarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (message.EhValido()) return message.ValidationResult;

            await _produtoRepository.Desabilitar(message.Nome);

            return await PersistirDados(_produtoRepository.UnitOfWork);
        }
    }
}
