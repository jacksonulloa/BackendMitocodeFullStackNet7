using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities
{
    public class Reserva : EntidadBase
    {
        public string NroTxn { get; set; } = "";
        public DateTime FechaTxn { get; set; } = DateTime.Now;
        public Titulo Titulo { get; set; } = new Titulo();
        public int TituloId { get; set; } = 0;
        public Cliente Cliente { get; set; } = new Cliente();
        public int ClienteId { get; set; } = 0;
        public int Cantidad { get; set; } = 0;
        public double PrecioUnitario { get; set; } = 0;
        public double ImporteTotal { get; set; } = 0;
        public string Moneda { get; set; } = "";
    }
}
