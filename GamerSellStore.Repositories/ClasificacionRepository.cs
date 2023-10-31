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
    public class ClasificacionRepository : RepositorioGenerico<Clasificacion>, IClasificacionRepository
    {
        public ClasificacionRepository(GamerSellStoreDbContext _context) : base(_context) { }
    }
}
