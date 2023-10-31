using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class PublisherSearchDtoResponse
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Pais { get; set; } = "";
        public int Estado { get; set; } = 0;
    }
}
