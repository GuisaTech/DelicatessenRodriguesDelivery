using DRD.WebApp.MVC.Models;
using DRD.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRD.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("Inicio")]
        public async Task<IActionResult> Inicio()
        {
            return View();
        }

        [HttpGet]
        [Route("")]
        [Route("Catalogo/Catalogo")]
        public async Task<IActionResult> Catalogo()
        {
            var categorias = await _catalogoService.ObterCategorias();
            return View(categorias);
        }

        [HttpGet]
        [Route("Catalogo/VerProdutos")]
        public async Task<IActionResult> ProdutosCategoria(string nome)
        {
            var categoria = await _catalogoService.ObterProdutosPelaCategoria(nome);
            return View(categoria);
        }

        #region categoria

        [HttpGet]
        [Route("catalogo/AdicionarCategoria")]
        public async Task<IActionResult> AdicionarCategoria()
        {
            return View();
        }

        [HttpPost]
        [Route("catalogo/AdicionarCategoria")]
        public async Task<IActionResult> AdicionarCategoria(CategoriaViewModel categoria)
        {
            var resposta = await _catalogoService.AdicionarCategoria(categoria);
            return View(new CategoriaViewModel());
        }

        #endregion

        #region Produto

        [HttpGet]
        [Route("catalogo/AdicionarProduto")]
        public async Task<IActionResult> AdicionarProduto()
        {
            ViewBag.categorias = await _catalogoService.ObterCategorias();

            return View();
        }

        [HttpPost]
        [Route("catalogo/AdicionarProduto")]
        public async Task<IActionResult> AdicionarProduto(ProdutoViewModel produto)
        {
            var resposta = await _catalogoService.AdicionarProduto(produto);

            return View(new ProdutoViewModel());
        }

        #endregion

        #region Endereço
        [HttpGet]
        [Route("catalogo/AdicionarEndereco")]
        public async Task<IActionResult> EnderecoPedido(string nomeProduto)
        {
            var pedido = new EnderecoViewModel() { NomeProduto = nomeProduto};
            return View(pedido);
        }

        [HttpPost]
        [Route("catalogo/AdicionarEndereco")]
        public async Task<IActionResult> EnderecoPedido(EnderecoViewModel pedido)
        {
            return View();
        }

        #endregion

        //[HttpGet]
        //[Route("")]
        //[Route("vitrine")]
        //public async Task<IActionResult> Index()
        //{
        //    var produtos = await _catalogoService.ObterTodos();

        //    return View(produtos);
        //}

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _catalogoService.ObterPorId(id);

            return View(produto);
        }
    }
}
