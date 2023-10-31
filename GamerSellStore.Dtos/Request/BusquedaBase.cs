using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class BusquedaBase
    {
        public int pagina { get; set; } = 0;
        public int filas { get; set; } = 0;
    }
}
