using DRD.ClienteTeste.API.Models;
using DRD.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DRD.ClienteTeste.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public Task<Cliente> ObterPorCpf(string cpf)
        {
            return _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
        }

        public void Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
