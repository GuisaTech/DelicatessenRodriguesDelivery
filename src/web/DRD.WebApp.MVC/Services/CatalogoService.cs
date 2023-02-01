using DRD.WebApp.MVC.Extensions;
using DRD.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace DRD.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);

            _httpClient = httpClient;
        }


        public async Task<CategoriaViewModel> AdicionarCategoria(CategoriaViewModel categoria)
        {
            StringContent content = ObterConteudo(categoria);

            var response = await _httpClient.PostAsync($"/categoria/adicionar", content);

            TratarErrosResponse(response);

            return categoria;

            //return await DeserializarObjetoResponse<CategoriaViewModel>(response);
        }

        public async Task<List<CategoriaViewModel>> ObterCategorias()
        {
            var response = await _httpClient.GetAsync($"/categorias");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<List<CategoriaViewModel>>(response);
        }

        public async Task<CategoriaViewModel> ObterProdutosPelaCategoria(string nome)
        {
            var response = await _httpClient.GetAsync($"/categoria/produtos?nome={nome}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CategoriaViewModel>(response);
        }

        public async Task<ProdutoViewModel> AdicionarProduto(ProdutoViewModel produto)
        {
            StringContent content = ObterConteudo(produto);

            var response = await _httpClient.PostAsync($"/catalogo/produto", content);

            TratarErrosResponse(response);

            return produto;

            //return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }








        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/catalogo/produtos/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }
    }
}
