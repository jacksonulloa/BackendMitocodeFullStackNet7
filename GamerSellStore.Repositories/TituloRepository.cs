using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GamerSellStore.Repositories
{
    public class TituloRepository : RepositorioGenerico<Titulo>, ITituloRepository
    {
        public TituloRepository(GamerSellStoreDbContext _context) : base(_context)
        {
            //context = _context;
        }
    }
}
