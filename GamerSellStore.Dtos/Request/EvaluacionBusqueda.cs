using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class EvaluacionBusqueda : BusquedaBase
    {
        public string Desde { get; set; } = DateTime.Now.ToShortDateString();
        public string Hasta { get; set; } = DateTime.Now.ToShortDateString();
        public string? titulo { get; set; } = "";
        public string? cliente { get; set; } = "";
    }
}
