using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class TituloBusqueda : BusquedaBase
    {
        public string? Titulo { get; set; } = null;
    }
}
