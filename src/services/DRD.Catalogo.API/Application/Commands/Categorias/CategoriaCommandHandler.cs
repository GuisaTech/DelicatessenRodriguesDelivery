using DRD.Catalogo.API.Models;
using DRD.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace DRD.Catalogo.API.Application.Commands.Categorias
{
    public class CategoriaCommandHandler : CommandHandler,
        IRequestHandler<CadastrarCategoriaCommand, ValidationResult>,
        IRequestHandler<AtualizarCategoriaCommand, ValidationResult>,
        IRequestHandler<AlterarDisponibilidadeCategoriaCommand, ValidationResult>,
        IRequestHandler<DesabilitarCategoriaCommand, ValidationResult>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ValidationResult> Handle(CadastrarCategoriaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var categoria = new Categoria(message.Nome, message.ImagemUrl, false);

            if (await _categoriaRepository.ExisteNoBanco(message.Nome))
            {
                AdicionarErro("Nome da categoria já existe");
                return ValidationResult;
            }

            _categoriaRepository.Adicionar(categoria);

            return await PersistirDados(_categoriaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarCategoriaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            if (await _categoriaRepository.ExisteNoBanco(message.Nome))
            {
                AdicionarErro("Nome da categoria já existe");
                return ValidationResult;
            }

            var categoria = await _categoriaRepository.ObterPorNome(message.NomeAntigo);

            if (categoria == null)
            {
                AdicionarErro("Houve um erro ao atualizar a categoria");
                return ValidationResult;
            }

            categoria.AtualizarNome(message.Nome);
            categoria.AtualizarUrlImagem(message.ImagemUrl);

            _categoriaRepository.Atualizar(categoria);

            return await PersistirDados(_categoriaRepository.UnitOfWork);

        }

        public async Task<ValidationResult> Handle(AlterarDisponibilidadeCategoriaCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            await _categoriaRepository.MudarStatusDisponibilidade(message.Nome);

            return await PersistirDados(_categoriaRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DesabilitarCategoriaCommand message, CancellationToken cancellationToken)
        {
            if (message.EhValido()) return message.ValidationResult;

            await _categoriaRepository.Desabilitar(message.Nome);

            return await PersistirDados(_categoriaRepository.UnitOfWork);
        }

    }
}
