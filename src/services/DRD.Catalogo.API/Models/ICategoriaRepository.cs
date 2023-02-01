using DRD.Core.Data;

namespace DRD.Catalogo.API.Models
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterTodos();
        Task<Categoria> ObterPorId(Guid id);
        Task<Categoria> ObterPorNome(string nome);
        Task<Categoria> ObterProdutosPelaCategoria(string nome);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);

        Task MudarStatusDisponibilidade(string nome);
        Task Desabilitar(string nome);

        Task<bool> ExisteNoBanco(string nome);

    }
}
