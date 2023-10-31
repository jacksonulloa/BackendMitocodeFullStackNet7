using GamerSellStore.Entities;
using GamerSellStore.Entities.Info;
using GamerSellStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamerSellStore.Repositories
{
    public class EvaluacionRepository : RepositorioGenerico<Evaluacion>, IEvaluacionRepository
    {
        public EvaluacionRepository(GamerSellStoreDbContext _context) : base(_context)
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

        public override async Task<int> AgregarAsync(Evaluacion entidad)
        {
            entidad.Fecha = DateTime.Now;
            await context.AddAsync(entidad);
            //Al no tener el savechanges no graba solo se esta agregando al contexto por lo tanto retornara 0 en el id
            return entidad.Id;
        }

        public override async Task ActualizarAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.ActualizarAsync();
        }
        public override async Task<Evaluacion?> BuscarPorIdAsync(int id)
        {
            return await context.Set<Evaluacion>()
                .Include(p => p.Cliente)
                .Include(p => p.Titulo)
                .ThenInclude(p => p.Genero)
                .Where(p => p.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<EvaluacionInfo>> ListarInfoAsync(string? titulo, string? publisher, string? genero, string? consola, CancellationToken cancellationToken = default)
        {
            //Enfoque Eager Loading
            return await context.Set<Evaluacion>()
                .Include(p => p.Titulo.Publisher)
            .Include(p => p.Titulo.Genero)
            .Include(p => p.Titulo.Consola)
            .Include(p => p.Titulo.Clasificacion)
            .Where(p => p.Titulo.Nombre.Contains(titulo ?? string.Empty)
                && p.Titulo.Publisher.Nombre.Contains(publisher ?? string.Empty)
                && p.Titulo.Genero.Nombre.Contains(genero ?? string.Empty)
                && p.Titulo.Consola.Nombre.Contains(consola ?? string.Empty))
            
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Select(p => new EvaluacionInfo
            {
                Id = p.Id,
                Cliente = p.Cliente.Nombre,
                FechaStr = p.Fecha.ToShortDateString(),
                HoraStr = p.Fecha.ToShortTimeString(),
                Titulo = p.Titulo.Nombre,
                Genero = p.Titulo.Genero.Nombre,
                Consola = p.Titulo.Consola.Nombre,
                Publisher = p.Titulo.Publisher.Nombre,
                Clasificacion = p.Titulo.Clasificacion.Nombre,
                CantidadDisponible = p.Titulo.Stock,
                PrecioUnitario = p.Titulo.Costo,
                Moneda = p.Titulo.Moneda,
                ImageUrl = p.Titulo.ImageUrl ?? string.Empty,
                Calificacion = p.Calificacion,
                Resenia = p.Resenia,
                Estado = p.Estado ? "Activo" : "Inactivo"
            })
            .ToListAsync(cancellationToken); // Esta linea ejecuta el query en la BD
        }
    }
}
