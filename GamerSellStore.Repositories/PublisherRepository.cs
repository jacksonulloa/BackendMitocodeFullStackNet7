using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Repositories
{
    public class PublisherRepository : RepositorioGenerico<Publisher>, IPublisherRepository
    {
        public PublisherRepository(GamerSellStoreDbContext _context) : base(_context) { }
    }
}
