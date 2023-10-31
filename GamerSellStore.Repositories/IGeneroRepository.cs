using GamerSellStore.Entities;

namespace GamerSellStore.Repositories
{
    public interface IGeneroRepository
    {
        Task ActualizarAsync();
        Task AgregarAsync(Genero objeto);
        Task<Genero?> BuscarPorIdAsync(int id);
        Task EliminarAsync(int id);
        Task<List<Genero>> ListarAsync();
    }
}