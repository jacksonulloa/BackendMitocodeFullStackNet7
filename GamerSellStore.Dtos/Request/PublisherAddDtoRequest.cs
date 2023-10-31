using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class PublisherAddDtoRequest
    {
        public string Nombre { get; set; } = "";
        public string Pais { get; set; } = "";
        public int Estado { get; set; } = 0;
    }
}
