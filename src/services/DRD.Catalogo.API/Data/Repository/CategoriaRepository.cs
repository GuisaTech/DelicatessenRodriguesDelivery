using DRD.Catalogo.API.Models;
using DRD.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DRD.Catalogo.API.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly CatalogoContext _context;

        public CategoriaRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Categoria>> ObterTodos()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<Categoria> ObterPorId(Guid id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(x => x.Id == id && !x.Removido);
        }

        public async Task<Categoria> ObterPorNome(string nome)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(x => x.Nome.ToLower() == nome.ToLower() && !x.Removido);
        }

        public async Task<Categoria> ObterProdutosPelaCategoria(string nome)
        {
            return await _context.Categorias.Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Nome.ToLower() == nome.ToLower() && !x.Removido);
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public async Task MudarStatusDisponibilidade(string nome)
        {
            var categoria = await ObterPorNome(nome);

            if (categoria != null)
            {
                categoria.AlterarDisponibilidade(!categoria.Disponivel);
                _context.Update(categoria);
            }
        }

        public async Task Desabilitar(string nome)
        {
            var categoria = await ObterPorNome(nome);

            if (categoria != null)
            {
                categoria.Disabilitar();
                _context.Update(categoria);
            }
        }

        public async Task<bool> ExisteNoBanco(string nome)
        {
            return await _context.Categorias
                .AnyAsync(x => x.Nome.ToLower() == nome.ToLower() && !x.Removido);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
