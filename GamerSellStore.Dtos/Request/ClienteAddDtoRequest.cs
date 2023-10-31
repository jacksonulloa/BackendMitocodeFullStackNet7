using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class ClienteAddDtoRequest
    {
        public string Correo { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Sexo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Pais { get; set; } = "";
        public int Estado { get; set; } = 0;
    }
}
