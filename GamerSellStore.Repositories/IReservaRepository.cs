using GamerSellStore.Entities;
using GamerSellStore.Entities.Info;

namespace GamerSellStore.Repositories
{
    public interface IReservaRepository : IRepositorioGenerico<Reserva>
    {
        Task CrearTransaccionAsync();
        Task RollbackTransaccionAsync();
        Task<ICollection<ReservaInfo>> ReporteReservaAsync(DateTime desde, DateTime hasta);
    }
}