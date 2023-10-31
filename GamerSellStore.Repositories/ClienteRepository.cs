using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamerSellStore.Repositories
{
    public class ClienteRepository : RepositorioGenerico<Cliente>, IClienteRepository
    {
        public ClienteRepository(GamerSellStoreDbContext _context) : base(_context)
        {
        }

        public async Task<Cliente?> BuscarPorEmailAsync(string email)
        {
            return await context.Set<Cliente>().FirstOrDefaultAsync(x=>x.Correo.Equals(email));
        }
    }
}
