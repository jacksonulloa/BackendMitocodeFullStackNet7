using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class TituloSearchDtoRequest
    {
        public string TituloNombre { get; set; } = "";
        public string PublisherNombre { get; set; } = "";
        public string ConsolaNombre { get; set; } = "";
        public string GeneroNombre { get; set; } = "";
        public string ClasificacionNombre { get; set; } = "";
        public int TituloEstado { get; set; } = -1;
    }
}
