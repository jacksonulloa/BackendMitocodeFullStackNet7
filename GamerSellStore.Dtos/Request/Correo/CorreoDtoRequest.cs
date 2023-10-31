using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request.Correo
{
    public class CorreoDtoRequest
    {
        public string Destinatario { get; set; } = "";
        public string Asunto { get; set; } = "";
        public string Mensaje { get; set; } = "";
    }
}
