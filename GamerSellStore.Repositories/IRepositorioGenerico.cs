using GamerSellStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Repositories
{
    public interface IRepositorioGenerico<TEntity> where TEntity : EntidadBase
    {
        Task<ICollection<TEntity>> ListarAsync();

        Task<ICollection<TEntity>> ListarAsync(Expression<Func<TEntity, bool>> predicado);

        Task<ICollection<TEntity>> ListarAsync<TKey>(Expression<Func<TEntity, bool>> predicado, Expression<Func<TEntity, TKey>> ordenadoPor);

        Task<ICollection<TEntity>> ListarAsync<TKey>(Expression<Func<TEntity, bool>> primerpredicado, Expression<Func<TEntity, bool>> segundopredicado, Expression<Func<TEntity, TKey>> ordenadoPor);

        Task<(ICollection<TInfo> Coleccion, int Total)> ListarAsync<TInfo, TKey>(
                Expression<Func<TEntity, bool>> predicado,
                Expression<Func<TEntity, TInfo>> selector,
                Expression<Func<TEntity, TKey>> ordenadoPor,
                int pagina, int filas, bool ascendente,
                string? relacionadocon = null
            );

        Task<ICollection<TInfo>> ListarAsync<TInfo, TKey>(
                Expression<Func<TEntity, bool>> predicado,
                Expression<Func<TEntity, TInfo>> selector,
                Expression<Func<TEntity, TKey>> ordenadoPor,
                bool ascendente,
                string? relacionadocon = null
            );

        Task<TEntity?> BuscarPorIdAsync(int id);

        Task<int> AgregarAsync(TEntity entidad);

        Task ActualizarAsync();

        Task EliminarAsync(int id);
    }
}
