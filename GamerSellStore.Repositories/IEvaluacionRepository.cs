using GamerSellStore.Entities;
using GamerSellStore.Entities.Info;

namespace GamerSellStore.Repositories
{
    public interface IEvaluacionRepository : IRepositorioGenerico<Evaluacion>
    {
        Task CrearTransaccionAsync();
        Task RollbackTransaccionAsync();
        Task<ICollection<EvaluacionInfo>> ListarInfoAsync(string? titulo, string? publisher, string? genero, string? consola, CancellationToken cancellationToken = default);
    }
}