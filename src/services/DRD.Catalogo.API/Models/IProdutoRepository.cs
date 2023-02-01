using DRD.Core.Data;

namespace DRD.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
        Task<Produto> ObterPorNome(string nome);
        Task<IEnumerable<Produto>> ObterPorNomeCategoria(string nomeCategoria);

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        Task MudarStatusDisponibilidade(string nome);
        Task Desabilitar(string nome);

        Task<bool> ExisteNoBanco(string nome);
    }
}
