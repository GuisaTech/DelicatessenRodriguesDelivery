using DRD.Catalogo.API.Models;
using DRD.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DRD.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.Include(x => x.Categoria)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(x => x.Id == id && !x.Removido);
        }

        public async Task<Produto> ObterPorNome(string nome)
        {
            return await _context.Produtos
                   .FirstOrDefaultAsync(x => x.Nome.ToLower() == nome.ToLower() && !x.Removido);
        }

        public async Task<IEnumerable<Produto>> ObterPorNomeCategoria(string nome)
        {
            return await _context.Produtos.Include(x=> x.Categoria)
                .Where(x => x.Categoria.Nome == nome.ToLower() && !x.Removido)
                .ToListAsync();
        }
        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public async Task MudarStatusDisponibilidade(string nome)
        {
            var produto = await ObterPorNome(nome);
            if (produto != null)
            {
                produto.AlterarDisponibilidade(!produto.Disponivel);
                _context.Update(produto);
            }
        }

        public async Task Desabilitar(string nome)
        {
            var produto = await ObterPorNome(nome);
            if (produto != null)
            {
                produto.Disabilitar();
                _context.Update(produto);
            }
        }

        public async Task<bool> ExisteNoBanco(string nome)
        {
            return await _context.Produtos.AnyAsync(x => x.Nome.ToLower() == nome.ToLower() && !x.Removido);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
