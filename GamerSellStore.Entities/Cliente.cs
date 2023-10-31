using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities
{
    public class Cliente : EntidadBase
    {
        public string Correo { get; set; } = "";
        public string Usuario { get; set; } = "";
        public string Sexo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Pais { get; set; } = "";
    }
}
