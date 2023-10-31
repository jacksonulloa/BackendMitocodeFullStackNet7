using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class ReservaReporteDtoRequest
    {
        public string Desde { get; set; } = DateTime.Now.ToShortDateString();
        public string Hasta { get; set; } = DateTime.Now.ToShortDateString();
    }
}
