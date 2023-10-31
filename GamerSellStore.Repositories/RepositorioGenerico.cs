using GamerSellStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Repositories
{
    public abstract class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : EntidadBase
    {
        protected readonly DbContext context;

        protected RepositorioGenerico(DbContext _context)
        {
            context = _context;
        }

        public virtual async Task ActualizarAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<int> AgregarAsync(TEntity entidad)
        {
            var objeto = await context.Set<TEntity>()
                .AddAsync(entidad);
            await context.SaveChangesAsync();
            return entidad.Id;
        }

        public virtual async Task<TEntity?> BuscarPorIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task EliminarAsync(int id)
        {
            var objeto = await context.Set<TEntity>().FindAsync(id);
            if(objeto is not null)
            {
                objeto.Estado = false;
                await ActualizarAsync();
            }
        }

        public async Task<ICollection<TEntity>> ListarAsync()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> ListarAsync(Expression<Func<TEntity, bool>> predicado)
        {
            return await context.Set<TEntity>()
                .Where(predicado)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> ListarAsync<TKey>(Expression<Func<TEntity, bool>> predicado, Expression<Func<TEntity, TKey>> ordenadoPor)
        {
            return await context.Set<TEntity>()
                .Where(predicado)
                .OrderBy(ordenadoPor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TEntity>> ListarAsync<TKey>(Expression<Func<TEntity, bool>> primerpredicado, Expression<Func<TEntity, bool>> segundopredicado, Expression<Func<TEntity, TKey>> ordenadoPor)
        {
            return await context.Set<TEntity>()
                .Where(primerpredicado)
                .Where(segundopredicado)
                .OrderBy(ordenadoPor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(ICollection<TInfo> Coleccion, int Total)> ListarAsync<TInfo, TKey>(
                Expression<Func<TEntity, bool>> predicado,
                Expression<Func<TEntity, TInfo>> selector,
                Expression<Func<TEntity, TKey>> ordenadoPor,
                int pagina, int filas, bool ascendente,
                string? relacionadocon = null
            )
        {
            var query = context.Set<TEntity>()
                                    .Where(predicado)
                                    .OrderBy(ordenadoPor)
                                    .Skip((pagina - 1) * filas)
                                    .Take(filas)
                                    //.Select(selector) //se retira por que afecta el include dado que se base en TEntity y no en TInfo
                                    .AsQueryable();

            if(!string.IsNullOrWhiteSpace(relacionadocon))
            {
                foreach(var tabla in relacionadocon.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(tabla);
                }
            }

            var coleccion = await query.Select(selector).ToListAsync(); // Aqui recien se ejecuta el query

            //var coleccion = await context.Set<TEntity>()
            //                        .Where(predicado)
            //                        .OrderBy(ordenadoPor)
            //                        .Select(selector)
            //                        .Skip((pagina - 1) * filas)
            //                        .ToListAsync();

            var total = await context.Set<TEntity>()
                        .Where(predicado).CountAsync();

            return (coleccion, total);
        }

        public async Task<ICollection<TInfo>> ListarAsync<TInfo, TKey>(
                Expression<Func<TEntity, bool>> predicado,
                Expression<Func<TEntity, TInfo>> selector,
                Expression<Func<TEntity, TKey>> ordenadoPor,
                bool ascendente,
                string? relacionadocon = null
            )
        {
            var query = context.Set<TEntity>()
                                    .Where(predicado)
                                    .OrderBy(ordenadoPor)
                                    .AsQueryable();

            if (!string.IsNullOrWhiteSpace(relacionadocon))
            {
                foreach (var tabla in relacionadocon.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(tabla);
                }
            }

            var coleccion = await query.Select(selector).ToListAsync(); // Aqui recien se ejecuta el query

            return coleccion;
        }
    }
}
