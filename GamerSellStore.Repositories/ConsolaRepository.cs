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
    public class ConsolaRepository : RepositorioGenerico<Consola>, IConsolaRepository
    {
        //private GamerSellStoreDbContext context;
        //public ConsolaRepository(GamerSellStoreDbContext _context)
        public ConsolaRepository(GamerSellStoreDbContext _context) : base(_context)
        {
            //context = _context;
        }
    }
}
