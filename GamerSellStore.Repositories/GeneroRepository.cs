using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private GamerSellStoreDbContext context;

        public GeneroRepository(GamerSellStoreDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Genero>> ListarAsync()
        {
            //return await context.Generos.ToListAsync();
            return await context.Set<Genero>().ToListAsync();
        }

        public async Task<Genero?> BuscarPorIdAsync(int id)
        {
            //return await context.Generos.FindAsync(id);
            return await context.Set<Genero>().FindAsync(id);
        }

        public async Task AgregarAsync(Genero objeto)
        {
            //await context.Generos.AddAsync(objeto);
            await context.Set<Genero>().AddAsync(objeto);
            await context.SaveChangesAsync();
        }

        //public async Task ActualizarAsync(int id, Genero objeto)
        public async Task ActualizarAsync()
        {
            //var registro = BuscarPorId(id);
            //if (registro is not null)
            //{
            //    registro.Nombre = objeto.Nombre;
            //    registro.Estado = objeto.Estado;
            //    context.SaveChanges();
            //}
            await context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            Genero? registro = await BuscarPorIdAsync(id);
            if (registro is not null)
            {
                registro.Estado = false;
                await ActualizarAsync();
            }
            else
            {
                throw new InvalidOperationException("El registro no puede ser eliminado");
            }
        }
    }
}
