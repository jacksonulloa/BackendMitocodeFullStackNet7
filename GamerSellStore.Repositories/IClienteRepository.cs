using GamerSellStore.Entities;

namespace GamerSellStore.Repositories
{
    public interface IClienteRepository : IRepositorioGenerico<Cliente>
    {
        Task<Cliente?> BuscarPorEmailAsync(string email);
    }
}