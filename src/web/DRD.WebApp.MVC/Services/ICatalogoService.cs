using DRD.WebApp.MVC.Models;
using Refit;

namespace DRD.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<List<CategoriaViewModel>> ObterCategorias();
        Task<CategoriaViewModel> AdicionarCategoria(CategoriaViewModel categoria);
        Task<ProdutoViewModel> AdicionarProduto(ProdutoViewModel produto);
        Task<CategoriaViewModel> ObterProdutosPelaCategoria(string nome);

    }

    public interface ICatalogoServiceRefit
    {
        [Get("/catalogo/produtos/")]
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();

        [Get("/catalogo/produtos/{id}")]
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}
