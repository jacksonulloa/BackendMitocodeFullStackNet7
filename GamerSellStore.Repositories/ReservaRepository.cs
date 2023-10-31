using Dapper;
using GamerSellStore.Entities;
using GamerSellStore.Entities.Info;
using GamerSellStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GamerSellStore.Repositories
{
    public class ReservaRepository : RepositorioGenerico<Reserva>, IReservaRepository
    {
        public ReservaRepository(GamerSellStoreDbContext _context) : base(_context)
        {
        }

        public async Task CrearTransaccionAsync()
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        public async Task RollbackTransaccionAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public override async Task<int> AgregarAsync(Reserva entidad)
        {
            entidad.FechaTxn = DateTime.Now;            
            //var ultimoValor = await context.Set<Reserva>().CountAsync() + 1;
            entidad.NroTxn = Utilitario.GenerarOperacion(entidad.FechaTxn);
            //agregando al contexto de forma explicita
            //await context.Set<Reserva>().AddAsync(entidad);
            //agregando al contexto de forma implicita
            await context.AddAsync(entidad);
            //Al no tener el savechanges no graba solo se esta agregando al contexto por lo tanto retornara 0 en el id
            return entidad.Id;
        }

        public override async Task ActualizarAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.ActualizarAsync();
        }
        public override async Task<Reserva?> BuscarPorIdAsync(int id)
        {
            return await context.Set<Reserva>()
                .Include(p => p.Cliente)
                .Include(p => p.Titulo)
                .ThenInclude(p => p.Genero)
                .Where(p => p.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<ReservaInfo>> ReporteReservaAsync(DateTime desde, DateTime hasta)
        {
            var query = await context.Database.GetDbConnection()
                .QueryAsync<ReservaInfo>(sql: "uspReporteReserva", commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        Desde = desde,
                        Hasta = hasta
                    });
            return query.ToList();
        }
    }
}
