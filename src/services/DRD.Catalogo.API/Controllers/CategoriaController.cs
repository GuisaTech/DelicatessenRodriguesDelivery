using DRD.Catalogo.API.Application.Commands.Categorias;
using DRD.Catalogo.API.Models;
using DRD.WebAPI.Core.Identidade;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DRD.Catalogo.API.Controllers
{
    [Authorize]
    public class CategoriaController : MainController
    {
        private readonly IMediator _mediator;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(IMediator mediator, ICategoriaRepository categoriaRepository)
        {
            _mediator = mediator;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        [Route("categorias")]
        [AllowAnonymous]
        public async Task<IActionResult> TodasCategorias()
        {
            return CustomResponse(await _categoriaRepository.ObterTodos());
        }

        [HttpGet]
        [Route("categoria/produtos")]
        [AllowAnonymous]
        public async Task<IActionResult> TodosProdutosPelaCategoria([FromQuery]string nome)
        {
            return CustomResponse(await _categoriaRepository.ObterProdutosPelaCategoria(nome));
        }

        [HttpGet]
        [Route("categorias/categoria/{id:Guid}")]
        public async Task<IActionResult> ObterPeloId(Guid id)
        {
            return CustomResponse(await _categoriaRepository.ObterPorId(id));
        }

        [HttpGet]
        [Route("categorias/categoria")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPeloNome(string nome)
        {
            return CustomResponse(await _categoriaRepository.ObterPorNome(nome));
        }

        [HttpPost]
        [Route("categoria/adicionar")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Adicionar(CategoriaViewModel model)
        {
            var command = new CadastrarCategoriaCommand(model.Nome, model.ImagemUrl);
            return CustomResponse(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("categoria/Atualizar")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Atualizar(CategoriaViewModel model, string NomeAntigo)
        {
            var command = new AtualizarCategoriaCommand(NomeAntigo, model.Nome, model.ImagemUrl);
            return CustomResponse(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("categoria/AlterarDisponibilidade")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> AlterarDisponibilidade([FromQuery]string nomeCategoria)
        {
            var command = new AlterarDisponibilidadeCategoriaCommand(nomeCategoria);
            return CustomResponse(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("categoria/Desabilitar")]
        [ClaimsAuthorize("Catalogo", "Admin")]
        public async Task<IActionResult> Desabilitar([FromBody]string nomeCategoria)
        {
            var command = new DesabilitarCategoriaCommand(nomeCategoria);
            return CustomResponse(await _mediator.Send(command));
        }

    }
}
