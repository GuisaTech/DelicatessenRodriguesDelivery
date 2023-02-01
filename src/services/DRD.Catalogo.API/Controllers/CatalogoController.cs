using DRD.Catalogo.API.Application.Commands.Produtos;
using DRD.Catalogo.API.Models;
using DRD.WebAPI.Core.Identidade;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DRD.Catalogo.API.Controllers
{
    [Authorize]
    public class CatalogoController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository, IMediator mediator)
        {
            _produtoRepository = produtoRepository;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await _produtoRepository.ObterTodos();
        }

        [HttpGet("catalogo/produtos/{id}")]
        [AllowAnonymous]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        [HttpGet("catalogo/produtosCategoria")]
        [AllowAnonymous]
        public async Task<IEnumerable<Produto>> ObterProdutosPelaCategoria(string nome)
        {
            return await _produtoRepository.ObterPorNomeCategoria(nome);
        }

        [HttpPost("catalogo/produto")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Adicionar(ProdutoViewModel model)
        {
            var command = new CadastrarProdutoCommand(model.Nome, model.Descricao,
                model.Valor, model.Imagem, model.QuantidadeEstoque,
                model.NomeCategoria, DateTime.Now);

            return CustomResponse(await _mediator.Send(command));
        }

        [HttpPost("catalogo/produto/Atualizar")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Atualizar(ProdutoViewModel model, string NomeAntigo)
        {
            var command = new AtualizarProdutoCommand(NomeAntigo, model.Nome, model.Descricao,
                model.Valor, model.Imagem, model.QuantidadeEstoque,
                model.NomeCategoria, DateTime.Now);

            return CustomResponse(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("catalogo/AlterarDisponibilidade")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> AlterarDisponibilidade([FromQuery] string nomeProduto)
        {
            var command = new AlterarDisponibilidadeProdutoCommand(nomeProduto);
            return CustomResponse(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("catalogo/Desabilitar")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Desabilitar([FromBody] string nomeProduto)
        {
            var command = new DesabilitarProdutoCommand(nomeProduto);
            return CustomResponse(await _mediator.Send(command));
        }

    }
}
