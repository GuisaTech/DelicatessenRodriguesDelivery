using DRD.Core.Data;

namespace DRD.Cliente.API.Models
{
    public interface IClienteRepository : IRepository<Clientes>
    {
        void Adicionar(Clientes cliente);

        Task<IEnumerable<Clientes>> ObterTodos();
        Task<Clientes> ObterPorCpf(string cpf);
    }
}
